//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;

//public class Dialog : MonoBehaviour
//{
//    public Text textComponemt;
//    public Image spriteComponent;
//    public string[] lines;
//    public Image[] actors;
//    public float textSpeed;

//    private int index;
//    // Start is called before the first frame update
//    void Start()
//    {
//        textComponemt.text = string.Empty;
//        StartDialog();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            if (textComponemt.text == lines[index] && spriteComponent == actors[index])
//            {
//                NextLine();
//            }
//            else
//            {
//                StopAllCoroutines();
//                textComponemt.text = lines[index];
//                spriteComponent = actors[index];
//            }
//        }
//    }

//    void StartDialog()
//    {
//        index = 0;
//        StartCoroutine(TypeLine());
//    }

//    IEnumerator TypeLine()
//    {
//        foreach (char c in lines[index].ToCharArray())
//        {
//            textComponemt.text += c;
//            yield return new WaitForSeconds(textSpeed);
//        }
//    }

//    void NextLine()
//    {
//        if (index < lines.Length - 1)
//        {
//            index++;
//            textComponemt.text = string.Empty;
//            StartCoroutine(TypeLine());
//        }
//        else
//        {
//            gameObject.SetActive(false);
//        }
//    }
//}
