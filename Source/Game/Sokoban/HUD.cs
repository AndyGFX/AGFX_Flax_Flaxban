using System;
using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;
using Game.Sokoban;

namespace Game;

/// <summary>
/// HUD Script.
/// </summary>
public class HUD : Script
{
    public UIControl ctrlLabel_0;
    public UIControl ctrlLabel_1;
    public UIControl ctrlLabel_2;
    public UIControl ctrlLabel_3;
    public UIControl ctrlLabel_4;
    public UIControl ctrlLabel_5;
    public Label label_0;
    public Label label_1;
    public Label label_2;
    public Label label_3;
    public Label label_4;
    public Label label_5;

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        
        this.label_0 = ctrlLabel_0.Get<Label>();
        this.label_1 = ctrlLabel_1.Get<Label>();
        this.label_2 = ctrlLabel_2.Get<Label>();
        this.label_3 = ctrlLabel_3.Get<Label>();    
        this.label_4 = ctrlLabel_4.Get<Label>();    
        this.label_5 = ctrlLabel_5.Get<Label>();    
        GLOBAL.HUD = Actor.GetScript<HUD>();
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame
    }
}
