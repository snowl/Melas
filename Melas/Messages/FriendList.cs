﻿using Melas.Structures;
using System;
using System.Collections.Generic;

namespace Melas.Messages
{
    public class FriendList : ServerMessage
    {
        public override byte ID => 20;

        public List<Friend> Friends { get; private set; }

        public override void Deserialize(ByteReader Data)
        {
            string a = Data.ReadString();
            String[] friends = a.Split('*');

            foreach (String str in friends)
            {
                String[] temp = str.Split(',');
                Friend friend = new Friend(int.Parse(temp[1]), temp[0]);
                friend.Status = (Status)(temp[2][0]);
            }
        }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
