using UnityEngine;

namespace Weapons.Great_Sword
{
    public class GreatSwordWeaponPrefab : WeaponPrefab
    {
        [SerializeField] private GameObject _vfx;
        [SerializeField] private Transform _weaponTransform;
        
        //Player by animation events
        public void PlayVFX()
        {
            // var vfx = Instantiate(_vfx, _weaponTransform.position + new Vector3(1, 0, 1), _weaponTransform.rotation);
            var vfx = Instantiate(_vfx, _weaponTransform.position, _weaponTransform.rotation);
            Destroy(vfx.gameObject, 1f);
        }
    }
}