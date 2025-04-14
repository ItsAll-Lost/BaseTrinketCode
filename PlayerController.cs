 Update()
    {
        RaycastHit hit;
        _groundedPlayer = Physics.Raycast(transform.position, Vector3.down, out hit, 0.9f);
        Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red); 

        if (_canMove)
        {
            Movement();
        }

       
        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _playerSpeed = 8f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _playerSpeed = 5f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Player hit by projectile.");
            FreezeControls(5f);
        }
        if (collision.gameObject.CompareTag("Draining"))
        {
            Debug.Log("Player On Trap.");
            _playerSpeed = 2f;

        }
    }
    void Movement()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput -= 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            verticalInput += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verticalInput -= 1f;
        }


        Vector3 movementInput = Quaternion.Euler(0, _cameraRig.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        _rigidbody.MovePosition(transform.position + movementDirection * _playerSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (_canMove)
        {
            _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * _gravityValue), ForceMode.VelocityChange);
        }



    }

    public void FreezeControls(float duration)
    {
        Debug.Log("Player frozen for " + duration + " seconds.");
        stunText.text = "<-STUNNED->";
        _canMove = false;
        Invoke("EnableControls", duration);
    }

    private void EnableControls()
    {
        stunText.text = "";
        _canMove = true;
    }
}

