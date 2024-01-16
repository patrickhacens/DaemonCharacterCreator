using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CharacterManager.Pages.Editors;


public abstract class BaseEditor<T> : ComponentBase
	where T : new()
{
	protected MudForm form = null!;

	public async Task<bool> Validate()
	{
		await form.Validate();
		return form.IsValid;
	}

	[Parameter]
	public virtual T Model { get; set; } = new();

	[Parameter]
	public bool ReadOnly { get; set; }

	public bool IsValid => form?.IsValid ?? false;
}
