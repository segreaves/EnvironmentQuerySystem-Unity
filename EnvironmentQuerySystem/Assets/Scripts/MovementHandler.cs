using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace EnvironmentQuerySystem
{
    public class MovementHandler : MonoBehaviour
    {
        [SerializeField] InputHandler _inputHandler;
        [SerializeField] float _speed = 10f;

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            Assert.IsNotNull(_inputHandler, "Error: InputHandler not found");
        }

        private void Update()
        {
            transform.position += new Vector3(_inputHandler.MovementValue.x, 0, _inputHandler.MovementValue.y) * _speed * Time.deltaTime;
        }
    }
}
