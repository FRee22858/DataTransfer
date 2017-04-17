using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;

namespace MasterServerLib
{
    public partial class IntegrationManager
    {
        //   当玩家点击任意一个红包墙内的红包时，读取脚本RedGift.cs
        //Function PropShow（TotalTimes，CurrentTimes）
        //    参数1：TotalTimes——玩家当前开启的红包总数 historycount
        //    参数2：CurrentTimes——玩家当日开启的红包数目 daylycount
        //    返回值：Return Value
        //        Value——掉落红包的星级
        //        Value的取值为1、2、3、4、5，分别代表1、2、3、4、5星红包
        //        Value通过表配置，来对应相关的红包道具
        static int[,] standard ={
{8700,1000,300,0,0},
{7900,1500,500,100,0},
{7500,1700,700,100,0},
{7000,2000,900,100,0},
{6450,2300,1100,150,0},
{5850,2700,1200,200,50},
{5200,3200,1300,250,50},
{4400,3850,1400,300,50},
{3600,4400,1500,400,100},
{2900,4900,1600,500,100},
{2200,5100,1900,600,200},
{2000,4000,3100,700,200},
{1800,3000,4200,800,200},
{1500,2800,4500,900,300},
{1200,2500,4100,1900,300},
{1000,2200,3600,2900,300},
{700,1900,3400,3600,400},
{400,1500,3400,4300,400},
{100,1100,3400,5000,400},
{0,700,3500,5300,500},
{0,400,4300,4800,500},
{0,0,4300,5200,500},
{0,0,4000,5000,1000},
{0,0,3000,6000,1000},
{0,0,0,8500,1500},
{0,0,0,8500,1500},
{0,0,0,8000,2000},
{0,0,0,0,10000}};
static int[,] correction ={
{0,0,0,0,0},
{-5000,0,0,0,0},
{-5000,0,0,0,0},
{-2000,0,0,0,0},
{-2000,-500,0,0,0},
{-2500,-1000,0,0,0},
{-3000,-1500,0,0,0},
{-2600,-2000,-400,0,0},
{-2400,-2400,-600,0,0},
{-2200,-2800,-800,0,0},
{-2000,-3000,-1400,0,0},
{-1900,-2900,-1500,0,0},
{-1700,-2000,-1800,0,0},
{-1500,-2000,-2100,0,0},
{-1200,-2100,-2800,-1000,0},
{-1000,-2100,-2500,-1200,0},
{-700,-1800,-2200,-1400,0},
{-400,-1400,-2200,-1700,0},
{-100,-1100,-2600,-2100,0},
{0,-700,-2900,-2700,0},
{0,-400,-4100,-3700,0},
{0,0,-4200,-4500,0},
{0,0,-3950,-4700,0},
{0,0,-3000,-4500,0},
{0,0,0,-7000,0},
{0,0,0,-7500,0},
{0,0,0,-7150,0},
{0,0,0,0,0}};
public static int PropShow(int TotalTimes, int CurrentTimes)
        {
            if (CurrentTimes<=0 ||TotalTimes <=0)
            {
                return 1;
            }
            int Value = 1;
            int CurrentRatio = 0;

            float Total = 0.00f;
            for (int i = 0; i < 5;i++ )
            {
                CurrentRatio = standard[TotalTimes - 1, i] + correction[CurrentTimes - 1, i];
                if (CurrentRatio <0)
                {
                    CurrentRatio = 0;
                }

                Total = Total + CurrentRatio;
            }
            int[] FinalRatio = {0,0,0,0,0};

            for (int j = 0; j < 5; j++)
            {
                float value = (standard[TotalTimes - 1, j] + correction[CurrentTimes - 1, j]) / Total * 10000.00f;
                CurrentRatio =(int)Math.Round(value, 0,MidpointRounding.AwayFromZero);

                if (CurrentRatio < 0)
                {
                    CurrentRatio = 0;
                }
                if (0==j) 
                {
                    FinalRatio[j] = CurrentRatio;
                }
                else if (4 == j)
                {
                    FinalRatio[j] = 10000;
                }
                else
                {
                    FinalRatio[j] = FinalRatio[j - 1] + CurrentRatio;
                }
            }

            int RandValue = RAND.Range(1, 10000);
            for (int k = 0; k < 5; k++)
            {
                if (RandValue <= FinalRatio[k])
                {
                    Value = k+1;
                    return Value;
                }
            }
            return Value;
        }
    }
}
