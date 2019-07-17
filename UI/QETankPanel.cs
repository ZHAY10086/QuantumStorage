﻿using BaseLibrary;
using BaseLibrary.UI;
using BaseLibrary.UI.Elements;
using ContainerLibrary;
using Microsoft.Xna.Framework;
using QuantumStorage.TileEntities;
using Terraria;
using Terraria.ModLoader;

namespace QuantumStorage.UI
{
	public class QETankPanel : BaseUIPanel<QETank>
	{
		private UITank tankFluid;

		private Frequency tempFrequency = new Frequency();

		private UIButton[] buttonsFrequency;
		private UITextButton buttonInitialize;

		public override void OnInitialize()
		{
			Width = (408, 0);
			Height = (172, 0);
			this.Center();

			UIText textLabel = new UIText("Quantum Entangled Tank")
			{
				HAlign = 0.5f
			};
			Append(textLabel);

			UITextButton buttonClose = new UITextButton("X")
			{
				Size = new Vector2(20),
				Left = (-20, 1),
				RenderPanel = false
			};
			buttonClose.OnClick += (evt, element) => BaseLibrary.BaseLibrary.PanelGUI.UI.CloseUI(Container);
			Append(buttonClose);

			if (!Container.frequency.IsSet)
			{
				buttonsFrequency = new UIButton[3];
				for (int i = 0; i < 3; i++)
				{
					int pos = i;
					buttonsFrequency[i] = new UIButton(ModContent.GetTexture("QuantumStorage/Textures/UI/EmptySocket"))
					{
						Size = new Vector2(16, 20),
						HAlign = 0.17f + 0.33f * pos,
						VAlign = 0.5f
					};
					buttonsFrequency[i].OnClick += (evt, element) =>
					{
						if (Utility.ValidItems.ContainsKey(Main.mouseItem.type) && tempFrequency != null)
						{
							tempFrequency[pos] = Utility.ValidItems[Main.mouseItem.type];
							buttonsFrequency[pos].texture = ModContent.GetTexture("QuantumStorage/Textures/Tiles/GemMiddle_0");
							buttonsFrequency[pos].sourceRectangle = new Rectangle(8 * (int)tempFrequency[pos], 0, 8, 10);
						}
					};
					Append(buttonsFrequency[i]);
				}

				buttonInitialize = new UITextButton("Initialize")
				{
					Width = (-64, 1),
					Height = (40, 0),
					VAlign = 1,
					HAlign = 0.5f
				};
				buttonInitialize.OnClick += (evt, element) =>
				{
					if (!tempFrequency.IsSet) return;

					Container.frequency = (Frequency)tempFrequency.Clone();

					for (int i = 0; i < 3; i++) RemoveChild(buttonsFrequency[i]);
					RemoveChild(buttonInitialize);

					AddTank();
				};
				Append(buttonInitialize);
			}
			else AddTank();
		}

		private void AddTank()
		{
			tankFluid = new UITank(Container)
			{
				Width = (40, 0),
				Height = (-44, 1),
				Top = (36, 0),
				HAlign = 0.5f
			};
			Append(tankFluid);
		}
	}
}