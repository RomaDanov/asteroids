using Configs.Weapons;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Contexts.Game.Components
{
	public class Equipments : MonoBehaviour
	{
		[SerializeField] private Transform weaponsRoot;

		private Dictionary<int, Weapon> weapons = new();

		public void Configure(IReadOnlyCollection<WeaponConfig> weapons, LayerMask targets)
		{
			ClearWeapons();
			for (int i = 0; i < weapons.Count; i++)
			{
				Weapon weapon = CreateWeapon(weapons.ElementAt(i), targets);
				this.weapons.Add(i, weapon);
			}
		}

		public Weapon GetWeapon(int slotId)
		{
			if (!weapons.TryGetValue(slotId, out Weapon weapon))
			{
				return null;
			}
			return weapon;
		}

		private Weapon CreateWeapon(WeaponConfig config, LayerMask targetLayers)
		{
			Weapon weapon = Instantiate(config.Prefab, weaponsRoot);
			weapon.Configure(config, targetLayers);
			return weapon;
		}

		private void ClearWeapons()
		{
			foreach (var kvp in weapons)
			{
				Weapon weapon = kvp.Value;
				Destroy(weapon.gameObject);
			}
			weapons.Clear();
		}
	}
}