using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] protected KeyCode dashPressed;
        [SerializeField] protected KeyCode sprintingHeld;
        [SerializeField] protected KeyCode jump;
        [SerializeField] protected KeyCode weaponFired;
        public virtual bool DashPressed()
        {
            if (Input.GetKeyDown(dashPressed))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool SprintingHeld()
        {
            if (Input.GetKey(sprintingHeld))
            {
                return true;
            }
            else { return false; }
        }
        public virtual bool JumpHeld()
        {
            if (Input.GetKey(jump))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool JumpPressed()
        {
            if (Input.GetKeyDown(jump))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool WeaponFired()
        {
            if (Input.GetKeyDown(weaponFired))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}