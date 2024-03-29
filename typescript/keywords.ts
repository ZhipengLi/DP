//    https://github.com/microsoft/TypeScript/issues/2536
//
interface Todo{
  id:number
}
interface ICaller{
  getJson():Promise<Todo>;
}
class JsonCaller extends String implements ICaller{ 
	  static staticMethod():void{
    	console.log("called from static method")
    }
	  constructor(str: string){
    	super(str)
    }
	  private url:string = "private string"
    public getJson():Promise<Todo>{
      let url = this.toString();
      if(!url){
       	throw new Error("empty url string")
      }
      console.log('received string:',this.toString());
    
      return new Promise((resolve) => {
        setTimeout(() => {
        	try{
              fetch(this.toString())
              .then(response=>resolve(response.json()))
            }
            catch(e){}
        }, 2000);
        console.log("private url:", this.url)
      });
    }
}

//debugger
//enum
//export
//import
//implements
//interface
//package
//protected
//public
async function f1() : Promise<number> {
  let caller : JsonCaller | null = new JsonCaller('https://jsonplaceholder.typicode.com/todos/1');
  //const x = await resolveAfter2Seconds(10);
  console.log('caller instance of JsonCaller?', caller instanceof JsonCaller)
  JsonCaller.staticMethod();
  const x:Todo = await caller.getJson();
  caller = null;
  console.log('json object:', x);
  return new Promise((resolve)=>{
  	resolve(x.id);
  });
}

f1().catch(e=>{console.log('error found:', e)}).then(x=>console.log('prosmise result of x.id:', x));

let testArray : (boolean | String | Number)[] = [true, String('2'), 100, 4, false]
console.log('2 in testArray?', '2' in testArray)
delete testArray[3]
console.log('after delete testArray[3]', testArray[3])
let i=0;
do{
	switch(typeof(testArray[i++])){
    	case 'boolean':
        	continue;
    	case 'string':
        	console.log('found a string')
        	break;
        case 'number':
        	if(testArray[i-1]<100){
            	console.log('num < 100')
            }
            else {
            	console.log('num>=100')
            }
        	break;
        default:
        	console.log('other type')
    }
    //i+=1;
} while(i<testArray.length)
