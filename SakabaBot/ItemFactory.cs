using System;
using System.Linq;

namespace SakabaBot
{
    static class ItemFactory
    {
        static string[] Material = new string[]
        {
            "石",
            "岩石",
            "尖晶石",
            "月長石",
            "柘榴石",
            "蛍石",
            "藍晶石",
            "魔石",
            "藍玉",
            "黄玉",
            "鋼玉",
            "紅玉",
            "黒玉",
            "翠玉",
            "蒼玉",
            "碧玉",
            "水晶",
            "紅水晶",
            "紫水晶",
            "琥珀",
            "珊瑚",
            "翡翠",
            "瑪瑙",
            "真珠",
            "ブラッドストーン",
            "ラピスラズリ",

            "銅",
            "青銅",
            "鉄",
            "鋼",
            "鉛",
            "鋼鉄",
            "鎖",
            "鏡",

            "骨",
            "獣骨",
            "魚鱗",
            "革",
            "ワニ革",
            "ヒドラ革",
            "蛇革",
            "毛皮",
            "象牙",
            "孔雀羽",
            "貝殻",
            "甲羅",

            "木",
            "カシ",
            "ヒノキ",
            "古木",
            "樹",
            "樹木",

            "ガラクタ",
            "マストドン鉱石",
        };
        static string[] MaterialRare = new string[]
        {
            "金",
            "白金",
            "黒金",
            "銀",
            "白銀",
            "オリハルコン",
            "ミスリル",
            "隕石",
            "竜鱗",
            "黒曜石",
            "ダイヤモンド",
        };

        static string[] Weapon = new string[]
        {
            "剣",
            "短剣",
            "大剣",
            "細剣",
            "ナイフ",
            "刀",
            "斧",
            "大斧",
            "槍",
            "双槍",
            "弓",
            "大弓",
            "矢",
            "杖",
            "爪",
            "鎧",
            "胸当て",
            "盾",
            "小手",
            "靴",
            "首飾り",
            "腕輪",
            "指輪",
            "お守り",
            "アミュレット",
            "サークレット",
            "地図",
            "鍵",
            "よく分からないもの",
            "偽物",
            "塊",
            "塊",
            "破片",
            "破片",
            "欠片",
            "欠片",
        };

        static string[] Option = new string[]
        {
            "壊れた",
            "壊れかけた",
            "呪われた",
            "古びた",
            "輝く",
            "高そうな",
            "欠けた",
            "血塗られた",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
        };


        static public string Create(int rank)
        {
            var random = new Random();
            if (random.Next(100) > 50)
            {
                return "";
            }

            var array = (rank < 2) ? Material : Material.Concat(MaterialRare).ToArray();
            string material = array[random.Next(array.Length)];
            string option = Option[random.Next(Option.Length)];
            string weapon = Weapon[random.Next(Weapon.Length)];

            return (random.Next(100) < 10)
                ? $"{option}{material}"
                : $"{option}{material}の{weapon}";
        }
    }
} 
