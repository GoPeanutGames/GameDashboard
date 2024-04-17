using System;
using System.Collections;
using PeanutDashboard._04_SpaceEscape.Model;
using UnityEngine;

namespace PeanutDashboard._04_SpaceEscape.Controllers
{
    public class SpaceEscapePlayerMovement : MonoBehaviour
    {
        private SpaceEscapeRunwayPartSide _partSide = SpaceEscapeRunwayPartSide.Center;
        private bool _jumping = false;
        private bool _jumpingEnabled = true;
        private float _finalPosY = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveRight();
            }

            if (Input.GetKeyUp(KeyCode.W) && _jumpingEnabled)
            {
                _jumping = true;
                _jumpingEnabled = false;
            }
        }

        private void FixedUpdate()
        {
            float diff = GetFinalPos() - this.transform.position.x;
            this.transform.Translate(new Vector3(diff *Time.deltaTime *2f, 0, 0));

            
            if (_jumping)
            {
                _jumping = false;
                this.GetComponent<Rigidbody>().AddForce(Vector3.up*5, ForceMode.Impulse);
            }
        }

        private void MoveLeft()
        {
            if (_partSide == SpaceEscapeRunwayPartSide.Center)
            {
                _partSide = SpaceEscapeRunwayPartSide.Left;
            }
            else if (_partSide == SpaceEscapeRunwayPartSide.Right)
            {
                _partSide = SpaceEscapeRunwayPartSide.Center;
            }
        }

        private void MoveRight()
        {
            if (_partSide == SpaceEscapeRunwayPartSide.Center)
            {
                _partSide = SpaceEscapeRunwayPartSide.Right;
            }
            else if (_partSide == SpaceEscapeRunwayPartSide.Left)
            {
                _partSide = SpaceEscapeRunwayPartSide.Center;
            }
        }

        private float GetFinalPos()
        {
            if (_partSide == SpaceEscapeRunwayPartSide.Center)
            {
                return 0;
            }
            if (_partSide == SpaceEscapeRunwayPartSide.Left)
            {
                return -1.2f;
            }
            return 1.2f;
        }

        private void OnCollisionEnter(Collision other)
        {
            _jumpingEnabled = true;
        }
    }
}