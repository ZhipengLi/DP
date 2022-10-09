//https://www.programiz.com/javascript/keywords-identifiers

class JsonCaller extends String{
	getJson(){
      console.log('received string:',this.toString());
    
      return new Promise((resolve) => {
        setTimeout(() => {
          fetch(this.toString())
              .then(response=>resolve(response.json()))
        }, 2000);
    });
    }
}

//debugger
//enum
//export
//import
async function f1() {
  const caller = new JsonCaller('https://jsonplaceholder.typicode.com/todos/1');
  //const x = await resolveAfter2Seconds(10);
  const x = await caller.getJson();
  console.log('json object:', x);
  return new Promise((resolve)=>{
  	resolve(x.id);
  });
}

f1().catch(e=>{console.log('error found:', e)}).then(x=>console.log('prosmise result of x.id:', x));

testArray = [true, String('2'), 100, 4]
delete testArray[3]
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
