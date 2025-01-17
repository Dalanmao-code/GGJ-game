using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEngine;

namespace UI
{
    public static class Atlas
    {
        private static Dictionary<string, SpriteAtlas> m_atlasCacheDict = new Dictionary<string, SpriteAtlas>();
        
        //立绘图标
        public static SpriteAtlas DrawingIcon
        {
            get
            {
                const string PATH = "UISprites/Drawing/Atlas";
                if (!m_atlasCacheDict.TryGetValue(PATH, out var atlas))
                {
                    atlas = Resources.Load<SpriteAtlas>(PATH);
                    m_atlasCacheDict[PATH] = atlas;
                }
                return atlas;
            }
        }
    }
}