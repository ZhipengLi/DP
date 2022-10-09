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

f1().then(x=>console.log('prosmise result of x.id:', x));
