using UnityEngine;
using System.Collections;


public class Move : MonoBehaviour {
	
	//Герой
	public GameObject Man;
	//Граница
	public GameObject LateralBoundaries;

	//Высота генерации нового блока
	private float height = 0f;
	
	//Границы области
	private int leftBound = -15;
	private int rightBound = 15;
	
	//Направление движения
	private string way = "";
	
	//флаг игры
	private bool play = false;
	
	//Сдвиг
	private float step = 1f;
	
	//Счётчик
	private int count = 0;
	
	private float time = 0f;
	
	private void OnGUI() {
		if (play)
		{
			if (Man.transform.position.y > 0)
			{
				GUIStyle Style = new GUIStyle();
				Style.fontSize = 30;
				Style.normal.textColor = Color.white;
				GUI.Label(new Rect(Screen.width/2 - 10, 50, 100, 50), (Man.transform.position.y).ToString(), Style);
			}
		}
	}
	
	private void OnMouseDown()
	{
		if 	((way == "") && (Man.transform.position.x == rightBound))
			way = "left";
				
		if 	((way == "") && (Man.transform.position.x == leftBound))
			way = "right";
	}
	
	// Use this for initialization
	void Start () {
		play = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (play)
		{
			Man.transform.position = new Vector3(Man.transform.position.x, 
												 Man.transform.position.y + step,
												 Man.transform.position.z);
			this.transform.position = new Vector3(this.transform.position.x, 
												  this.transform.position.y + step,
												  this.transform.position.z);
			
			if (Input.acceleration.x > 0.2)
			{
				if 	((way == "") && (Man.transform.position.x == leftBound))
					way = "right";
			}
			
			if (Input.acceleration.x < -0.2)
			{
				if 	((way == "") && (Man.transform.position.x == rightBound))
					way = "left";
			}
			if (Input.GetKeyDown(KeyCode.D) && (way == "") && (Man.transform.position.x != rightBound))
			{
				way = "right";
			}
			
			if (Input.GetKeyDown(KeyCode.A) && (way == "") && (Man.transform.position.x != leftBound))
			{
				way = "left";
			}
			
			if (way == "right")
				if (Man.transform.position.x != rightBound)
				{
					Man.transform.position = new Vector3(Man.transform.position.x + 3f, 
													 	 Man.transform.position.y + step,
														 Man.transform.position.z);
					this.transform.position = new Vector3(this.transform.position.x, 
														  this.transform.position.y + step,
														  this.transform.position.z);
				}
				else
					way = "";
			
			if (way == "left")
				if (Man.transform.position.x != leftBound)
				{
					Man.transform.position = new Vector3(Man.transform.position.x - 3f, 
													 	 Man.transform.position.y + step,
														 Man.transform.position.z);
					this.transform.position = new Vector3(this.transform.position.x, 
														  this.transform.position.y + step,
														  this.transform.position.z);
				}
				else
					way = "";
			
			if (Man.transform.position.y > height)
			{
				count ++;
				height += 100f;
				GameObject.Instantiate(GameObject.Find("LateralBoundaries" + (count - 1).ToString())).name = "LateralBoundaries" + count.ToString();
				GameObject.Find("LateralBoundaries" + count.ToString()).transform.position = new Vector3(
												LateralBoundaries.transform.position.x,
												height,
												LateralBoundaries.transform.position.z);
				if (count > 2)
				{
					Destroy(GameObject.Find("LateralBoundaries" + (count - 2).ToString()));
				}
			}
		}
	}
}
