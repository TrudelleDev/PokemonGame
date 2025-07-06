using PokemonGame.Pokemons;
using PokemonGame.Shared.Interfaces;
using PokemonGame.Systems.UI.Summary;
using Sirenix.OdinInspector;
using System;

using UnityEngine;

/// <summary>
/// Coordinates the UI components in the Pokémon summary screen,
/// including the always-visible header and switchable content tabs (Info, Skills, Moves).
/// Handles data binding and unbinding for each section to ensure accurate display
/// based on the currently selected Pokémon.
/// </summary>
[Serializable]
public class SummaryTabGroup : IPokemonBind, IUnbind
{
    [SerializeField, Required]
    [Tooltip("Displays the Pokémon's name, level, gender, and visual representation.")]
    private SummaryHeader header;

    [SerializeField, Required]
    [Tooltip("Tab displaying general information about the selected Pokémon.")]
    private SummaryInfoTab infoTab;

    [SerializeField, Required]
    [Tooltip("Tab displaying the Pokémon's skill stats such as HP, Attack, and Defense.")]
    private SummarySkillTab skillTab;

    [SerializeField, Required]
    [Tooltip("Tab displaying the Pokémon's moves and related details.")]
    private SummaryMoveTab moveTab;

    /// <summary>
    /// Binds the provided Pokémon data to all UI sections in the summary screen.
    /// If the Pokémon or its core data is null, all sections are cleared instead.
    /// </summary>
    /// <param name="pokemon">The Pokémon instance to display in the summary screen.</param>
    public void Bind(Pokemon pokemon)
    {
        if (pokemon?.Data == null)
        {
            Unbind();
            return;
        }

        header.Bind(pokemon);
        infoTab.Bind(pokemon);
        skillTab.Bind(pokemon);
        moveTab.Bind(pokemon);
    }

    /// <summary>
    /// Clears all UI sections by unbinding any previously bound Pokémon data.
    /// </summary>
    public void Unbind()
    {
        header.Unbind();
        infoTab.Unbind();
        skillTab.Unbind();
        moveTab.Unbind();
    }
}
