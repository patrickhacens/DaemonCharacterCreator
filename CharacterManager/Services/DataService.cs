using Daemon.Models;
using System.Text;
using CsvHelper.Configuration;
using CsvHelper;
using System.Diagnostics;
using MudBlazor;

namespace CharacterManager.Services;

public class DataService
{
	private readonly HttpClient httpClient;
	private readonly ILogger<DataService> logger;
	private readonly ISnackbar snackbar;
	private readonly CsvConfiguration csvConfig;

	public DataService(HttpClient httpClient, ILogger<DataService> logger, ISnackbar snackbar)
	{
		this.httpClient=httpClient;
		this.logger=logger;
		this.snackbar=snackbar;
		csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
		{
			IgnoreBlankLines =true,
			HasHeaderRecord = true,
			HeaderValidated = null,
			MissingFieldFound = null,
			AllowComments = true,
		};
	}

	private async Task<T[]> GetData<T>(string urlPart, CancellationToken cancellation)
	{
		try
		{

			using var response = await httpClient.GetAsync($"data/{urlPart}.csv", cancellation);
			using var stream = await response.Content.ReadAsStreamAsync(cancellation);
			using var sr = new StreamReader(stream, Encoding.UTF8);
			using CsvReader reader = new(sr, csvConfig);

			return reader.GetRecords<T>().ToArray();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error while getting data");
			snackbar.Add("Error while getting data", Severity.Error);
			throw;
		}
	}

	public Task<Skill[]> GetWeaponSkills(CancellationToken cancellation = default)
		=> GetData<Skill>("WeaponSkills", cancellation);

	public Task<Skill[]> GetSkills(CancellationToken cancellation = default)
		=> GetData<Skill>("Skills", cancellation);

	public Task<Skill[]> GetCombatSkills(CancellationToken cancellation = default)
		=> GetData<Skill>("CombatSkills", cancellation);

	public Task<Armor[]> GetArmors(CancellationToken cancellation = default)
		=> GetData<Armor>("Armors", cancellation);

	public Task<Weapon[]> GetWeapons(CancellationToken cancellation = default)
		=> GetData<Weapon>("Weapons", cancellation);

}
