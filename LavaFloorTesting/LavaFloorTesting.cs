using Modding;
using System;
using UnityEngine;

namespace LavaFloorTesting
{
    public class LavaFloorTestingMod : Mod
    {
        private static tk2dSprite? sprite;

        private static LavaFloorTestingMod? _instance;

        internal static LavaFloorTestingMod Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(LavaFloorTestingMod)} was never constructed");
                }
                return _instance;
            }
        }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public LavaFloorTestingMod() : base("LavaFloorTesting")
        {
            _instance = this;
        }

        public override void Initialize()
        {
            Log("Initializing");

            On.HeroController.Start += OnHCStart;
            ModHooks.HeroUpdateHook += CheckGrounded;

            Log("Initialized");
        }

        private void OnHCStart(On.HeroController.orig_Start orig, HeroController self)
        {
            orig(self);

            sprite = self.GetComponent<tk2dSprite>();
        }

        private void CheckGrounded()
        {
            if (HeroController.instance.cState.onGround)
            {
                sprite!.color = Color.red;
            }
            else
            {
                sprite!.color = Color.white;
            }
        }
    }
}
