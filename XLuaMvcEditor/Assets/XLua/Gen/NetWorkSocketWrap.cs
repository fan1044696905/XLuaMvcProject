#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class NetWorkSocketWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(NetWorkSocket);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Connect", _m_Connect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DisConnect", _m_DisConnect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendMsg", _m_SendMsg);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnConnectOK", _g_get_OnConnectOK);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnConnectOK", _s_set_OnConnectOK);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					NetWorkSocket gen_ret = new NetWorkSocket();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to NetWorkSocket constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Connect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NetWorkSocket gen_to_be_invoked = (NetWorkSocket)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _ip = LuaAPI.lua_tostring(L, 2);
                    int _port = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.Connect( _ip, _port );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisConnect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NetWorkSocket gen_to_be_invoked = (NetWorkSocket)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DisConnect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendMsg(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                NetWorkSocket gen_to_be_invoked = (NetWorkSocket)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
                    
                    gen_to_be_invoked.SendMsg( _buffer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnConnectOK(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NetWorkSocket gen_to_be_invoked = (NetWorkSocket)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnConnectOK);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnConnectOK(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                NetWorkSocket gen_to_be_invoked = (NetWorkSocket)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnConnectOK = translator.GetDelegate<System.Action>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
