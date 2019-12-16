﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decodedic 
{
    public string type;
    public int room_id=1;
    public string content;
    public int[] cards;
    public int player;
    public string[] names=new string[4];
    string[] sop = {"chi1","chi2","chi3","peng","gang","hu" };
    public bool[] butt = new bool[7];//special op 's message
    public int[][] chipai = new int[3][];
    public string room;
    public bool[] readystatement = new bool[4];
    // Start is called before the first frame updbuttate
    public void decodeinstruction(Dictionary<string,string> dic)
    {

        type = dic["type"];
        if (dic.ContainsKey("player"))
        {
            player =int.Parse( dic["player"]);
            player = (player + 4 - room_id) % 4;
        
        }
        if (dic.ContainsKey("room"))
        {
            room = dic["room"];

        }
        
        
        if (type.Equals("card")| type.Equals("pair")|type.Equals("initcard")|type.Equals("college"))
        {
            content = dic["content"];
            string[] cardlist = content.Split(' ');
            cards = new int[cardlist.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = int.Parse(cardlist[i]);
            }
        }else if (type.Equals("specialope"))
        {
            for(int i = 0; i < 6; i++)
            {
                Debug.Log(sop.Length);
                Debug.Log(sop[i]);
                content = dic[sop[i]];
                Debug.Log("content: "+content);
                butt[i] = false;
                if (content != null)
                {
                    
                    string[] cardlist = content.Split(' ');
                    butt[i] = true;
                    int[] cardsy = new int[cardlist.Length];
                    Debug.Log(cardlist);
                    for (int j = 0; j < cardlist.Length; j++)
                    {
                        cardsy[j] = int.Parse(cardlist[j]);
                    }

                    if (i < 3)
                    {
                        chipai[i] = cardsy;
                    }

                    if (i == 5)
                    {
                        if (content.Equals("0"))
                        {
                            butt[5] = false;
                        }
                        else
                        {
                            butt[5] = true;
                        }
                    }
                    
                }
                
            }
            

        }else if (type.Equals("cpg")|type.Equals("play"))
        {
            content = dic["card"];
            string[] cardlist = content.Split(' ');
            cards = new int[cardlist.Length];
            for (int i = 0; i < cardlist.Length; i++)
            {
                cards[i] = int.Parse(cardlist[i]);
            }
        }else if (type.Equals("id"))
        {
            Debug.Log("your socket_id is :"+dic["content"]);
            PlayerPrefs.SetString("socket_id",dic["content"]);
        }else if (type.Equals("roominfo"))
        {
            PlayerPrefs.SetString("room",dic["room"]);
            PlayerPrefs.SetString("room_id",dic["room_id"]);
            room_id = int.Parse(dic["room_id"]);
            
            string content = dic["name"];
            string[] na = content.Split(' ');
            content = dic["ready"];
            string[] bol = content.Split(' ');
           
            
            for (int i = 1; i <= 4; i++)
            {
                int pl = (i + 4 - room_id) % 4;
                if (na[i-1].Equals("_"))
                {
                    
                    names[pl] = "等待加入";
                }
                else
                {
                    names[(i + 4 - room_id) % 4] = na[i-1];
                }

                if (bol[i-1].Equals("0"))
                {
                    readystatement[pl] = false;
                }
                else
                {
                    readystatement[pl] = true;
                }
                    
            }
        }

    }
    



}