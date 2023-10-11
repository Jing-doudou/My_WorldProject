using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerText : MonoBehaviour
{
    public Camera eye;
    float speedMove = 6;//�ƶ��ٶ�
    float speedH = 5f;//�����ƶ��ٶ�
    float speedAngle = 250;//��ת���ٶ�
    public CharacterController cc;//��ɫ������

    float minAngle = -90;//̧ͷ��߽Ƕ� 
    float maxAngle =90;
    float yRote;
    const float G = -5f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }
    private void Move()
    {
        //�ƶ���
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        cc.Move(transform.forward * Time.deltaTime * y * speedMove);
        cc.Move(transform.right * Time.deltaTime * x * speedH);
        if (true)
        {
            float xRote = Input.GetAxis("Mouse X");
            transform.Rotate(transform.up * speedAngle * xRote * 2f * Time.deltaTime);

            yRote -= Input.GetAxis("Mouse Y");
            yRote = Mathf.Clamp(yRote, minAngle, maxAngle);
            eye.transform.localEulerAngles = new Vector3(yRote, 0, 0);//��������ĽǶȣ�
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //  Rigidbody.AddRelativeForce(Vector3.up,ForceMode.Impulse)
            cc.Move(-transform.up * 2.5f * G * Time.deltaTime);
        }
        else
        {
            cc.Move(transform.up * G * Time.deltaTime);
        }
    }
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit ray;
            if (Physics.Raycast(eye.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out ray))
            {
                ray.transform.GetComponent<BlockBase>().Hp -= 1;
            }
        }
    }

}
