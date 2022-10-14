// https://www.youtube.com/watch?v=LKVHFHJsiO0&list=PLNqp92_EXZBJYFrpEzdO2EapvU0GOJ09n

let userName: string = "Jack";
let hasloggedIn: boolean = true;

userName += " Herrington";
console.log(userName);
let myNumber: number = 18;
let myRegex: RegExp = /foo/;
const names:string[] = userName.split(" ");
const myValues: Array<number> = [1,2,3];
interface Person {
    first: string;
    last: string;
}
const myPerson: Person = {
  first:"Jack",
  last: "Herrington"
}
const ids: Record<number, string> = {
  10: "a",
  20: "b"
}
ids[30] = "c";
if (ids[30]==="c"){}

for (let i=0;i<10;i++){
  console.log(i);
}
[1,2,3].forEach((v)=>console.log(v));
const out = [4,5,6].map((v)=>`${v*10}`);
