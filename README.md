# study__delegate_Reference #

## 環境 ##
*	OS X El Capitan(10.11.6)
*	Xcode : 7.2
*	unity : 5.3.0f4
*	oF	  : 0.9.0

## add on ##

## purpose/motivation ##
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

## Contents ##
*	FuncPtrの代用 : delegate  
*	delegateの型宣言 : c++のtypdef  
		delegate void SampleDelegate(int param);  
		
*	delegateの変数宣言と初期化  
		SampleDelegate sample_delegate = new SampleDelegate(Func);  
		or  
		SampleDelegate sample_delegate = Func; // C#2.0以降では、変数名から暗黙の型変換をしてくれるように  
		
*	delegate(参照型)の代入に関する考察(paper)  
*	構造体 memberにdelegateを入れる  
*	上記、構造体の多次元配列 : 定義と初期化  
		要素数分 埋めないとNG : classでも、blankはNG. nullで埋めるのはok.  
		structの場合、nullは指定できない(classの時はok)  
		  
*	上記、多次元配列ないのdelegateへaccessしてFunction call.  

	
## Device ##

## note ##






