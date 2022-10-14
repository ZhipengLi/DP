// https://www.youtube.com/watch?v=ixCxoFAoOps&list=PLNqp92_EXZBJYFrpEzdO2EapvU0GOJ09n

function printToFile(text:string, callback:()=>void):void{
  console.log(text);
  callback();
}

type MutationFunction = (v: number)=>number;

function arrayMutate(
  numbers:number[],
  mutate: MutationFunction
):number[]{
  return numbers.map(mutate);
}

const myNewMutateFunc: MutationFunction = (v:number)=>v*100;

console.log(arrayMutate([1,2,3], (v)=>v*10))

type AdderFunction = (v:number)=>number;
function createAdder(num: number) : AdderFunction{
  return (val:number)=>num+val;
}
const addOne = createAdder(1);
console.log(addOne(55));
