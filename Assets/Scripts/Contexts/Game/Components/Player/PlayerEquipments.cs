using Configs.Weapons;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Contexts.Game.Components.Player
{
	public class PlayerEquipments : MonoBehaviour
	{
		[SerializeField] private Transform weaponsRoot;

		private Dictionary<int, Weapon> weapons = new();

		public void Configure(IReadOnlyCollection<WeaponConfig> weapons)
		{
			ClearWeapons();
			for (int i = 0; i < weapons.Count; i++)
			{
				Weapon weapon = CreateWeapon(weapons.ElementAt(i));
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

		private Weapon CreateWeapon(WeaponConfig config)
		{
			Weapon weapon = Instantiate(config.Prefab, weaponsRoot);
			weapon.Configure(config);
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