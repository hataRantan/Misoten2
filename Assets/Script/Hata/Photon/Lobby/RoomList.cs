using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;

// IEnumerable<RoomInfo>インターフェースを実装して、foreachでルーム情報を列挙できるようにする
public class RoomList : IEnumerable<RoomInfo>
{
    private Dictionary<string, RoomInfo> dictionary = new Dictionary<string, RoomInfo>();

    public void Update(List<RoomInfo> changedRoomList)
    {
        foreach (var info in changedRoomList)
        {
            if (!info.RemovedFromList)
            {
                dictionary[info.Name] = info;
            }
            else
            {
                dictionary.Remove(info.Name);
            }
        }
    }

    public void Clear()
    {
        dictionary.Clear();
    }

    // 指定したルーム名のルーム情報があれば取得する
    public bool TryGetRoomInfo(string roomName, out RoomInfo roomInfo)
    {
        return dictionary.TryGetValue(roomName, out roomInfo);
    }

    public IEnumerator<RoomInfo> GetEnumerator()
    {
        foreach (var kvp in dictionary)
        {
            yield return kvp.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
