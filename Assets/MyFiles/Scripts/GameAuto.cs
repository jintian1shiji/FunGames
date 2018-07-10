using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//------------测试git----------- 1****
public class GameAuto : MonoBehaviour {
    public static GameAuto instance;
    public GameObject _parent;
    private GameObject childParent = null;

    public static GameObject tempParent;

    private List<int> disabledList = new List<int>();
    public static int topicInt = 0;
    private bool isEnter = false;

    private GameObject targetBtn = null;
    private GameObject targetTemp = null;

    private Quaternion quaBtn;
    private float rotZ = 0;

    public static bool isWin = true;

    public GameObject winText;

    void Start () {
        Init(3, 1, 5, 74);
        tempParent = _parent;
        instance = this;
    }
	
	void Update () {
		if (isEnter)
		{
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("按下");
                targetBtn = targetTemp;
            }
            if (Input.GetMouseButtonDown(1) && targetBtn != null)
            {
                if (rotZ == 0)
                {
                    rotZ = 90;
                } 
                else
                {
                    rotZ = 0;
                }
                quaBtn.eulerAngles = new Vector3(0, 0, rotZ);
                targetBtn.GetComponent<RectTransform>().localRotation = quaBtn;

                if (!targetBtn.GetComponent<DragScript>().isRot)
                {
                    targetBtn.GetComponent<DragScript>().isRot = true;
                }
                else targetBtn.GetComponent<DragScript>().isRot = false;
            }
		}
	}

    //jj = 0 为- =1 为+
    void Init(int x,int jj,int y, int result)
    {
        int lengT = x.ToString().Length + y.ToString().Length + result.ToString().Length + 2;

        int le = 100;
        int sInt = 0;
        int i = 0;
        int j = 0;
        for (i = 0; i < x.ToString().Length; i++)
        {
            CreateNum(int.Parse(x.ToString().Substring(i, 1)), i*90+ le);
        }
        i += 1;
        sInt += i;
        CreateAD(jj, sInt * 90 + le);

        for (j = 0; j < y.ToString().Length; j++)
        {
            CreateNum(int.Parse(y.ToString().Substring(j, 1)), (j + sInt + 2) * 90+ le);
        }
        j += 1;
        sInt += j+2;
        CreateEQ(sInt * 90 + le);

        for (int k = 0; k < result.ToString().Length; k++)
        {
            CreateNum(int.Parse(result.ToString().Substring(k, 1)), (sInt + k + 2) * 90+ le);
        }

        OnListenBtn();
        ChangeBtnName();
    }

    //左式创建
    void CreateNum(int num,int step)
    {
        if (disabledList.Count > 0)
        {
            disabledList.Clear();
        }

        //把资源加载到内存中
        Object ob2d = Resources.Load("preMatch", typeof(GameObject));
        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
        childParent = Instantiate(ob2d) as GameObject;
        childParent.transform.SetParent(_parent.transform);
        childParent.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(step, 0, 0);
        childParent.transform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        switch (num)
        {
            case 0:
                disabledList.Add(0);//隐藏中间一根
                break; ;
            case 1:
                disabledList.Add(0);
                disabledList.Add(1);
                disabledList.Add(2);
                disabledList.Add(5);
                disabledList.Add(6);
                break;
            case 2:
                disabledList.Add(3);
                disabledList.Add(6);
                break;
            case 3:
                disabledList.Add(3);
                disabledList.Add(4);
                break;
            case 4:
                disabledList.Add(2);
                disabledList.Add(4);
                disabledList.Add(5);
                break;
            case 5:
                disabledList.Add(1);
                disabledList.Add(4);
                break;
            case 6:
                disabledList.Add(1);
                break;
            case 7:
                disabledList.Add(0);
                disabledList.Add(3);
                disabledList.Add(4);
                disabledList.Add(5);
                break;
            case 8:
                break;
            case 9:
                disabledList.Add(4);
                break;
            default:
                break;
        }

        if (disabledList.Count > 0)
        {
            foreach (Transform child in childParent.transform)
            {
                string nm = child.name.Substring(child.name.Length - 1, 1);
                for (int i =0;i < disabledList.Count;i++)
                {
                    if (int.Parse(nm) == disabledList[i])
                    {
                        child.gameObject.SetActive(false);
                        break;
                    }
                }
                
            }
        }
    }

    //创建+ -
    void CreateAD(int ad,int step)
    {
        if (ad == 1)//+
        {
            //把资源加载到内存中
            Object ob2d = Resources.Load("A", typeof(GameObject));
            //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
            childParent = Instantiate(ob2d) as GameObject;
            childParent.transform.SetParent(_parent.transform);
            childParent.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(step, 0, 0);
            childParent.transform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        } 
        else//-
        {
            //把资源加载到内存中
            Object ob2d = Resources.Load("D", typeof(GameObject));
            //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
            childParent = Instantiate(ob2d) as GameObject;
            childParent.transform.SetParent(_parent.transform);
            childParent.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(step, 0, 0);
            childParent.transform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    //创建 =
    void CreateEQ(int eq)
    {
            //把资源加载到内存中
            Object ob2d = Resources.Load("EQ", typeof(GameObject));
            //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
            childParent = Instantiate(ob2d) as GameObject;
            childParent.transform.SetParent(_parent.transform);
            childParent.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(eq, 0, 0);
            childParent.transform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    void OnListenBtn()
    {
        foreach (Transform ts in _parent.transform)
        {
            if (ts.gameObject.name == "preMatch(Clone)")
            {
                OnlistenButton(ts.gameObject);
            }
        }
    }

    //监听父物体下的所有子物体按钮事件
    private void OnlistenButton(GameObject obj)
    {
        Button btn;
        foreach (Transform child in obj.transform)
        {
            child.gameObject.AddComponent<DragScript>();
            //Debug.Log("所有该脚本的物体下的子物体名称:" + child.name);
            btn = child.GetComponent<Button>();
            //lambda表达式转换为委托类型  
            btn.onClick.AddListener(delegate () { this.OnBtnClick(child.name); });

            UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener>();
            btnListener.OnClick += delegate (GameObject gb)
            {
                //Debug.Log("按下：  " + gb.name);

            };
            btnListener.OnMouseEnter += delegate (GameObject gb)
            {
                //Debug.Log("进入：  " + gb.name);
                isEnter = true;
                targetTemp = gb;
            };
            btnListener.OnMouseExit += delegate (GameObject gb)
            {
                //Debug.Log("离开：  " + gb.name);
                isEnter = false;
                //targetTemp = null;
                //targetBtn = null;
            };

        }
    }

    public void OnBtnClick(string value)
    {

    }

    void ChangeBtnName()
    {
        int x = 0;
        foreach (Transform ts in _parent.transform)
        {
            if (ts.gameObject.name == "preMatch(Clone)")
            {
                x++;
                switch (topicInt)
                {
                    case 0:
                        if (x == 3)
                        {
                            CHangeWinName(ts,"Button2");
                        }
                        break;
                    case 1:
                        if (x == 2)
                        {
                            CHangeWinName(ts, "Button3");
                        }
                        break;
                    case 2:
                        if (x == 1)
                        {
                            CHangeWinName(ts, "Button0");
                        }
                        break;
                    case 3:
                        if (x == 3)
                        {
                            CHangeWinName(ts, "Button2");
                        }
                        break;
                    case 4:
                        if (x == 2)
                        {
                            CHangeWinName(ts, "Button1");
                        }
                        break;
                }
            }
        }
    }


    void CHangeWinName(Transform ts,string nm)
    {
        foreach (Transform el in ts)
        {
            if (el.gameObject.name == nm)
            {
                el.gameObject.name = "wBtn";
                //Debug.Log(el.gameObject.name + " " + nm);
            }
        }
    }

    public void EnabledWin(string nm)
    {
        if (!winText.activeSelf)
        {
            winText.SetActive(true);
            winText.GetComponent<Text>().text = nm;
            Invoke("DisabledWin", 1.5f);
        }
    }

    void DisabledWin()
    {
        winText.SetActive(false);
    }

    public void StartNextLevel()
    {
        if (topicInt >= 4)
        {
            return;
        }
        StartCoroutine("StartDestroyChild"); 
    }

    void DestroyChild()
    {
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            Destroy(_parent.transform.GetChild(i).gameObject);
        }
    }

    IEnumerator StartDestroyChild()
    {
        yield return new WaitForSeconds(0.05f);

        //Method 1
        while (_parent.transform.childCount > 0)
        {
            //Debug.Log(_parent.transform.childCount);
        
            Destroy(_parent.transform.GetChild(0).gameObject);
        
            yield return new WaitForEndOfFrame();
        }

        //Method 2
        DestroyChild();
        ///没有变为0
        //Debug.Log(" 111111111  " + _parent.transform.childCount);
     
        yield return new WaitForEndOfFrame();
        //Debug.Log(" 222222222 " + _parent.transform.childCount);

        NextLevel();
    }

    void NextLevel()
    {
        targetBtn = null;
        targetTemp = null;
        topicInt++;

        switch (topicInt)
        {
            case 0:
                Init(3, 1, 5, 74);
                break;
            case 1:
                Init(74, 0, 4, 4);
                break;
            case 2:
                Init(8, 0, 7, 7);
                break;
            case 3:
                Init(33, 0, 76, 11);
                break;
            case 4:
                Init(1, 1, 8, 1);
                break;
        }
    }
}
