//https://www.programiz.com/javascript/keywords-identifiers

function resolveAfter2Seconds(x) {
  return new Promise((resolve) => {
    const url = 'https://jsonplaceholder.typicode.com/todos/1';
    setTimeout(() => {
      fetch(url)
          .then(response=>resolve(response.json()))
    }, 2000);
  });
}

async function f1() {
  const x = await resolveAfter2Seconds(10);
  console.log('json object:', x);
  return new Promise((resolve)=>{
  	resolve(x.id);
  });
}

f1().catch(e=>{console.log('error found:', e)}).then(x=>console.log('prosmise result of x.id:', x));

testArray = [1, String('2'), 3]
for(let i=0;i<testArray.length;i++){
	switch(typeof(testArray[i])){
    	case 'string':
        	console.log(typeof(testArray[i]), 'found a string')
        	break;
        default:
        	console.log(typeof(testArray[i]))
    }
}

