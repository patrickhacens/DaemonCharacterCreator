using Daemon.Models;
using System.Text;
using CsvHelper.Configuration;
using CsvHelper;

namespace CharacterManager.Services;

public class DataService
{
	private readonly HttpClient httpClient;
	private readonly CsvConfiguration csvConfig;

	public DataService(HttpClient httpClient)
	{
		this.httpClient=httpClient;
		csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
		{
			IgnoreBlankLines =true,
			HasHeaderRecord = true,
			HeaderValidated = null,
			//MissingFieldFound = null,
			AllowComments = true,
		};
	}

	private async Task<T[]> GetData<T>(string urlPart, CancellationToken cancellation)
	{
		using var response = await httpClient.GetAsync($"data/{urlPart}.csv", cancellation);
		using var stream = await response.Content.ReadAsStreamAsync(cancellation);
		using var sr = new StreamReader(stream, Encoding.UTF8);
		using CsvReader reader = new(sr, csvConfig);

		return reader.GetRecords<T>().ToArray();
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
