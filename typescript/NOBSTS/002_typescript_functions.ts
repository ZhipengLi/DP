// https://www.youtube.com/watch?v=-TsIUuA3yyE&list=PLNqp92_EXZBJYFrpEzdO2EapvU0GOJ09n

function addNumbers(a:number, b:number):number {
  return a+b;
}
console.log(addNumbers(1,2));

const addStrings = (str1:string, str2:string=""):string =>`${str1} ${str2}`;
console.log(addStrings("a","b"));

const format=(title:string, param:string | number):string =>`${title} ${param}`
console.log(format("a", 1));

const fetchData = (url: string):Promise<string> => Promise.resolve(`Data from ${url}`);
function introduce(salutation: string, ...names:string[]):string{
  return `${salutation} ${names.join(" ")}`;
}
