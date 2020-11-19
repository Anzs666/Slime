using System.Collections;
using System.Collections.Generic;
using Common;
using UGUI.Framework;
using UnityEngine;
using UGUI.Package;
public class UIPackageWindow : UIWindow
{
    #region 事件添加
    protected override void OnAddListener()
    {
        GetChildUIEventListener("CloseButton").onClick += CloseButtonClick;
    }
    protected override void OnRemoveListener()
    {
        GetChildUIEventListener("CloseButton").onClick -= CloseButtonClick;
    }
    private void CloseButtonClick(GameObject gameObject)
    {
        UIManager.Instance.CloseWindow(UIWindowType.Package);
    }
    #endregion

    public int MaxCapacity = 36;                            //总格子数  
    public int PerGirdMaxWeight = 10;                         //每一个格子的最大储存数\
    public int PerGirdMaxAmount = 10;
    public List<GameObject> grids = new List<GameObject>(); //表示整个格子列表
    public List<Item> items = new List<Item>();             //表示所有物品列表


    /// <summary>
    /// 动态生成背包格子
    /// </summary>
    void Start()
    {
        for (int i = 0; i < MaxCapacity; i++)
        {
            grids.Add(Instantiate(ResourcesManager.Load<GameObject>("Package_Grid")));//克隆格子
            grids[i].transform.SetParent(TransformHelper.FindChildByName(transform, "LayoutPanel"));//将格子放在流式布局的Panel底下
            if (grids[i].GetComponent<UGUI.Package.Grid>() == null) grids[i].AddComponent<UGUI.Package.Grid>();
            grids[i].GetComponent<UGUI.Package.Grid>().gridID = i;//将格子编号
            grids[i].GetComponent<UGUI.Package.Grid>().MaxItemAmount = PerGirdMaxAmount;//设置最大储存数
            grids[i].GetComponent<UGUI.Package.Grid>().MaxItemWeight = PerGirdMaxWeight;
            items.Add(new Item());
        }

    }
    /// <summary>
    /// 返回合适的格子
    /// </summary>
    /// <returns></returns>
    public UGUI.Package.Grid FindSuitableGrid(int weight)
    {
        foreach (GameObject g in grids)
        {
            UGUI.Package.Grid grid = g.GetComponent<UGUI.Package.Grid>();
            if (!grid.Isfilled()&&!grid.IsOverWeight(weight))
            {
                return grid;
            }
        }
        return null;
    }

    /// <summary>
    /// 查找存放相同物品的格子
    /// </summary>
    /// <returns></returns>
    public UGUI.Package.Grid FindSameTypeGrid()
    {
        return null;
    }

    /// <summary>
    /// 添加到指定格子
    /// </summary>
    /// <returns></returns>
    public bool AddItemToCurGrid(int _id)
    {
        return false;
    }

    /// <summary>
    /// 将itemDatabase中相应id的物品按顺序添加到装备栏中
    /// </summary>
    /// <param name="_id"></param>
    public bool AddItem(Item item)
    {
        if (item == null)
        {
            return false;
        }
        UGUI.Package.Grid grid = FindSuitableGrid(item.Weight);
        if (grid == null)
        {
            Debug.LogWarning("没有空的物品槽");
            return false;
        }
        else
        {
            grid.AddItem(item);
            return true;
        }

    }

    /// <summary>
    /// 卸载指定位置的物品
    /// </summary>
    /// <param name="_id"></param>
    public void DropItem(int _id)
    {

    }

    public void DropAllItem()
    {

    }
}
