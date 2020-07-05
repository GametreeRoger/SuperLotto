using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public TextAsset BlackListAsset;
    public Text MainText;
    public Text SubText;
    private HashSet<int> mMainBlackList;
    private HashSet<int> mSubBlackList;
    private List<int> mMainLotto;
    private List<int> mSubLotto;
    int[] mainArray;
    int[] subArray;

    void Start()
    {
        mMainLotto = new List<int>();
        for (int i = 1; i <= 38; i++)
        {
            mMainLotto.Add(i);
        }

        mSubLotto = new List<int>();
        for (int i = 1; i <= 8; i++)
        {
            mSubLotto.Add(i);
        }


        mMainBlackList = new HashSet<int>();
        mSubBlackList = new HashSet<int>();
        if (null != BlackListAsset && !string.IsNullOrEmpty(BlackListAsset.text))
            parseText(BlackListAsset.text);

        mainArray = mMainLotto.ToArray();
        subArray = mSubLotto.ToArray();
        mainArray = removeBlacklistNumber(mainArray, mMainBlackList);
        subArray = removeBlacklistNumber(subArray, mSubBlackList);
    }

    public void StartRandom()
    {
        System.Random ran = new System.Random();
        ran.Shuffle(mainArray);
        ran.Shuffle(subArray);

        setText(MainText, mainArray, 6);
        setText(SubText, subArray, 1);
    }

    private int[] removeBlacklistNumber(int[] array, HashSet<int> blacklist)
    {
        int count = 0;
        for(int i = 0; i < array.Length; i++)
        {
            if(blacklist.Contains(array[i]))
            {
                count++;
                array[i] = 0;
            }
        }
        if(count > 0)
        {
            List<int> newArray = new List<int>();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != 0)
                    newArray.Add(array[i]);
            }

            return newArray.ToArray();
        }

        return array;
        
    }

    private void parseText(string text)
    {
        string[] strlist = text.Split('\n');
        for(int i = 0; i < strlist.Length; i++)
        {
            Debug.Log(strlist[i]);
            string[] numlist = strlist[i].Split(',');
            for(int j = 0; j < numlist.Length; j++)
            {
                if(j != numlist.Length - 1)
                {
                    mMainBlackList.Add(Convert.ToInt32(numlist[j]));
                }
                else
                {
                    mSubBlackList.Add(Convert.ToInt32(numlist[j]));
                }
            }
        }
    }

    private void printSet(HashSet<int> list)
    {
        string str = "";
        foreach(int num in list)
        {
            str += num.ToString() + ", ";
        }
        Debug.Log(str);
    }

    private void printArray(int[] array)
    {
        string str = "";
        for(int i = 0; i < array.Length; i++)
        {
            str += array[i].ToString() + ", ";
        }
        Debug.Log(str);
    }

    private void setText(Text numText, int[] array, int length)
    {
        if (1 == length)
            numText.text = array[0].ToString();
        else
        {
            string str = "";
            string split = ", ";
            for (int i = 0; i < length; i++)
            {
                if (i == length - 1)
                    split = string.Empty;
                str += array[i].ToString() + split;
            }
            numText.text = str;
        }

    }
}


static class RandomExtensions
{
    public static void Shuffle<T>(this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}