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
    public class MMO_MemoryStreamWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(MMO_MemoryStream);
			Utils.BeginObjectRegister(type, L, translator, 0, 20, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadShort", _m_ReadShort);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteShort", _m_WriteShort);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadUShort", _m_ReadUShort);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteUShort", _m_WriteUShort);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadInt", _m_ReadInt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteInt", _m_WriteInt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadUInt", _m_ReadUInt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteUInt", _m_WriteUInt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadLong", _m_ReadLong);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteLong", _m_WriteLong);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadULong", _m_ReadULong);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteULong", _m_WriteULong);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadFloat", _m_ReadFloat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteFloat", _m_WriteFloat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadDouble", _m_ReadDouble);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteDouble", _m_WriteDouble);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadBool", _m_ReadBool);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteBool", _m_WriteBool);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReadUTF8String", _m_ReadUTF8String);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WriteUTF8String", _m_WriteUTF8String);
			
			
			
			
			
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
					
					MMO_MemoryStream gen_ret = new MMO_MemoryStream();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING))
				{
					byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
					
					MMO_MemoryStream gen_ret = new MMO_MemoryStream(_buffer);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to MMO_MemoryStream constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadShort(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        short gen_ret = gen_to_be_invoked.ReadShort(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteShort(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    short _value = (short)LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.WriteShort( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadUShort(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        ushort gen_ret = gen_to_be_invoked.ReadUShort(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteUShort(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ushort _value = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.WriteUShort( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadInt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int gen_ret = gen_to_be_invoked.ReadInt(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteInt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _value = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.WriteInt( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadUInt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        uint gen_ret = gen_to_be_invoked.ReadUInt(  );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteUInt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint _value = LuaAPI.xlua_touint(L, 2);
                    
                    gen_to_be_invoked.WriteUInt( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadLong(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        long gen_ret = gen_to_be_invoked.ReadLong(  );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteLong(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    long _value = LuaAPI.lua_toint64(L, 2);
                    
                    gen_to_be_invoked.WriteLong( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadULong(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        ulong gen_ret = gen_to_be_invoked.ReadULong(  );
                        LuaAPI.lua_pushuint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteULong(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ulong _value = LuaAPI.lua_touint64(L, 2);
                    
                    gen_to_be_invoked.WriteULong( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFloat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        float gen_ret = gen_to_be_invoked.ReadFloat(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteFloat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.WriteFloat( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadDouble(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        double gen_ret = gen_to_be_invoked.ReadDouble(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteDouble(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    double _value = LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.WriteDouble( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadBool(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.ReadBool(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteBool(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _value = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.WriteBool( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadUTF8String(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.ReadUTF8String(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteUTF8String(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                MMO_MemoryStream gen_to_be_invoked = (MMO_MemoryStream)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.WriteUTF8String( _str );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
