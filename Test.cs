/************************************************************
purpose/ motivation
	音楽連動system = Gushaにて、Unity上でもmoving Lightを実装したい。
	そのため、vj_Unity側でも、下記のようなTableから
		Table[NUM_STATE][NUM_MAX_CANDIDATE]
			[State0]	weight	*Func
						weight	*Func
										...NUM_MAX_CANDIDATE
			[State1]
			...
			
	Dice @ State change from Candidateし、
	この結果に基づいて各moving LightのAnimationを管理することとなった。
	
本scriptで研究したこと/ わかること
	-	FuncPtrの代用 : delegate
	-	delegateの型宣言 : c++のtypdef
			delegate void SampleDelegate(int param);
			
	-	delegateの変数宣言と初期化
			SampleDelegate sample_delegate = new SampleDelegate(Func);
			or
			SampleDelegate sample_delegate = Func; // C#2.0以降では、変数名から暗黙の型変換をしてくれるように
			
	-	delegate(参照型)の代入に関する考察(paper)
	-	構造体 memberにdelegateを入れる
	-	上記、構造体の多次元配列 : 定義と初期化
			要素数分 埋めないとNG : classでも、blankはNG. nullで埋めるのはok.
			structの場合、nullは指定できない(classの時はok)
			
	-	上記、多次元配列ないのdelegateへaccessしてFunction call.
************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************
************************************************************/
public class Test : MonoBehaviour {
	/****************************************
	****************************************/
	delegate void SampleDelegate(int param);
	
	/****************************************
	****************************************/
	class PARAM{
		public readonly int val; // これをreadonlyにしないと、下記で、paramは新たにnewなど、できないものの、param.valはaccess okになってしまう。
		
		public PARAM(int _val){
			val = _val;
		}
	};
	
	struct STR_TEST{
		private readonly int Weight;
		private readonly SampleDelegate sample_delegate;
		private readonly PARAM param;
		
		public STR_TEST(int _Weight, SampleDelegate _sample_delegate)
		{
			Weight = _Weight;
			sample_delegate = _sample_delegate;
			
			param = new PARAM(100);
		}
		
		public int get_Weight(){
			return Weight;
		}
		
		/******************************
		******************************/
		public SampleDelegate get_SampleDelegate(){
			return sample_delegate;
		}
		
		/******************************
		******************************/
		public PARAM get_PARAM(){
			return param;
		}
		
		/******************************
		******************************/
		public void print_param(){
			Debug.Log("param = " + param.val);
		}
		
		/******************************
		Deep Copy.
		******************************/
		public void Copy_SampleDelegate(out SampleDelegate dele){
			dele = new SampleDelegate(sample_delegate);
		}
	}
	
	/****************************************
	****************************************/
	static STR_TEST[,] StrTest = new STR_TEST[2,3]{
		{
			new STR_TEST(0, new SampleDelegate(Func1)),
			new STR_TEST(1, new SampleDelegate(Func2)),
			new STR_TEST(1, new SampleDelegate(Func2)),
		},
		{
			new STR_TEST(0, new SampleDelegate(Func1)),
			new STR_TEST(0, new SampleDelegate(Func1)),
			new STR_TEST(0, new SampleDelegate(Func1)),
		}
	};
	
	/****************************************
	****************************************/
	
	/******************************
	******************************/
	void Start () {
		// Debug.Log(StrTest.Length);
	}
	
	/******************************
	******************************/
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha0)){
			Debug.Log("Weight=" + StrTest[0, 0].get_Weight() + ", ");
			
			SampleDelegate dele = StrTest[0, 0].get_SampleDelegate();
			dele(0);
			
		}else if(Input.GetKeyDown(KeyCode.Alpha1)){
			Debug.Log("Weight=" + StrTest[0, 1].get_Weight() + ", ");
			
			SampleDelegate dele;
			StrTest[0, 1].Copy_SampleDelegate(out dele);
			dele(0);
			
		}else if(Input.GetKeyDown(KeyCode.Alpha2)){
			PARAM param = StrTest[0, 0].get_PARAM();
			// param.val = 99; // readonlyなのでCompile Error.
			StrTest[0, 0].print_param();
		}
	}
	
	/******************************
	******************************/
	public static void Func1(int param){
		Debug.Log("Func1:" + param);
	}
	
	/******************************
	******************************/
	public static void Func2(int param){
		Debug.Log("Func2:" + param);
	}
}
