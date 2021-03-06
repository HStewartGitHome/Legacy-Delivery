﻿using System;
using System.Collections.Generic;

namespace DeliverySupport.Services
{
    public class NextImage : INextImage
    {
        public int CurrentIndex { get; set; }
        public int CurrentCount { get; set; }
        Dictionary<int, CardContext> cards = new Dictionary<int, CardContext>();

   

        private static readonly string[] legacyImages = new[]
        {
               "legacy/mainscreen.jpg",
               "legacy/sendmessage.jpg",
               "legacy/addcustomer.jpg",
               "legacy/newitem.jpg",
               "legacy/mainwithorder.jpg",
               "legacy/saveorder.jpg",
               "legacy/test.jpg"
        };

        private static readonly string[] legacyTitles = new[]
        {
            "Main C++ Legacy Screen",
            "Send Message C++ Legacy Screen",
            "Add Customer C++ Legacy Screen",
            "Add New Item C++ Legacy Screen",
            "Main C++ Legacy Screen with Order",
            "Save Order C++ Legacy Screen",
            "Legacy Test Screen"
        };

        private static readonly string[] legacyXML = new[]
        {
               "",
               "legacy/sendmessage_xml.jpg",
               "",
               "",
               "",
               "legacy/saveorder_xml.jpg",
               ""
        };

        private static readonly string[] modernImages = new[]
        {
            "modern/orderschema.png",
            "modern/itemschema.png",
            "modern/messagesschema.png",
            "modern/legacyvsmodern_order.jpg"
        };

        private static readonly string[] modernTitles = new[]
        {
            "Schema for Order contents",
            "Schema for Item contents",
            "Schema for Message contents",
            "Legacy C++ Modern Sequence Diagram"
        };

        private static readonly string[] legacyWinFormImages = new[]
      {
               "legacy/winformmain.png",
               "legacy/winformsendmessage.png",
               "legacy/winformaddcust.png",
               "legacy/winformadditem.png",
               "legacy/winformmainorder.png",
               "legacy/winformsaveorder.png"
        };

        private static readonly string[] legacyWinFormXML = new[]
 {
               "",
               "legacy/winformsendmessage_xml.png",
               "",
               "",
               "",
               "legacy/winformsaveorder_xml.png"
        };

        private static readonly string[] legacyWinFormTitles = new[]
        {
            "Main WinForm Legacy Screen",
            "Send Message WinForm Legacy Screen",
            "Add Customer WinForm Legacy Screen",
            "Add New Item WinForm Legacy Screen",
            "Main WinForm Legacy Screen with Order",
            "Save Order WinForm Legacy Screen"
        };

        public void Initialize(int cardType)
        {
            CurrentIndex = 0;
            if (cardType == 0)
                CurrentCount = 7;
            else if (cardType == 1)
                CurrentCount = 7;
            else if (cardType == 2)
                CurrentCount = 4;
            else if (cardType == 6)
                CurrentCount = 6;
            else if (cardType == 7)
                CurrentCount = 6;
            else
                CurrentCount = 0;

            SetCardContext(cardType);
        }


        public string Next(int cardType)
        {
            GetCardContext(cardType);
            CurrentIndex++;
            if (CurrentIndex > (CurrentCount - 2))
                CurrentIndex = CurrentCount - 1;
            SetCardContext(cardType);

            return ImageAtCurrentIndex(cardType);
        }

        public string Previous(int cardType)
        {
            GetCardContext(cardType);
            if (CurrentIndex > 0)
            {
                CurrentIndex--;
                SetCardContext(cardType);
            }

            return ImageAtCurrentIndex(cardType);
        }

        public string ImageAtCurrentIndex(int cardType)
        {
            GetCardContext(cardType);
            if (cardType == 0)
                return legacyImages[CurrentIndex];
            else if (cardType == 1)
                return legacyXML[CurrentIndex];
            else if (cardType == 2)
                return modernImages[CurrentIndex];
            else if (cardType == 6)
                return legacyWinFormImages[CurrentIndex];
            else if (cardType == 7)
                return legacyWinFormXML[CurrentIndex];
            else
                return "";
        }

        public string TitleAtCurrentIndex(int cardType)
        {
            GetCardContext(cardType);
            if (cardType == 0)
                return legacyTitles[CurrentIndex];
            else if (cardType == 1)
                return ImageAtCurrentIndex(cardType);
            else if (cardType == 6)
                return legacyWinFormTitles[CurrentIndex];
            else
                return "";
        }

        public string TextAtCurrentIndex(int cardType)
        {
            GetCardContext(cardType);
            return CurrentIndex.ToString();
        }

        internal void SetCardContext(int cardType)
        {
            CardContext context = new CardContext
            {
                CardType = cardType,
                Index = CurrentIndex,
                Count = CurrentCount
            };

            cards[cardType] = context;

            if ((cardType == 0) || (cardType == 6))
                cards[cardType + 1] = context;
        }

        internal void GetCardContext(int cardType)
        {
            CardContext context = cards[cardType];
            if (context == null)
            {
                CurrentIndex = 0;
                CurrentCount = 0;
            }
            else
            {
                CurrentIndex = context.Index;
                CurrentCount = context.Count;
            }


        }
    }
}
