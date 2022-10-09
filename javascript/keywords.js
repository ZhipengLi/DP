//https://www.programiz.com/javascript/keywords-identifiers

class JsonCaller{
	getJson(){
      return new Promise((resolve) => {
      const url = 'https://jsonplaceholder.typicode.com/todos/1';
      setTimeout(() => {
        fetch(url)
            .then(response=>resolve(response.json()))
      }, 2000);
    });
    }
}


async function f1() {
  const caller = new JsonCaller();
  //const x = await resolveAfter2Seconds(10);
  const x = await caller.getJson();
  console.log('json object:', x);
  return new Promise((resolve)=>{
  	resolve(x.id);
  });
}

f1().catch(e=>{console.log('error found:', e)}).then(x=>console.log('prosmise result of x.id:', x));

testArray = [true, String('2'), 3, 4]
let i=0;
do{
	switch(typeof(testArray[i])){
    	case 'string':
        	console.log(typeof(testArray[i]), 'found a string')
        	break;
        default:
        	console.log(typeof(testArray[i]))
    }
    i+=1;
} while(i<testArray.length)

