function myForEach<T>(list:T[], callback:(item:T)=>void):void{
    list.reduce((prev, cur)=>{
        callback(cur);
        return undefined;
    }, undefined);
}
let myList:Array<number> = [1,2,3];
myForEach(myList, (item)=>{console.log(item)});

function myMap<T,K>(list:T[], callback:(item:T)=>K):K[]{
    let res:K[] = [];
    list.reduce((prev, cur)=>{
        let newItem = callback(cur);
        res.push(newItem);
        return undefined;
    }, undefined);
    return res;
}
myList = [1,2,3];
let myNewList = myMap(myList, (item)=>(item*10));
myNewList.forEach(item=>{console.log(item)});

function myFilter<T>(list:T[], callback:(item:T)=>boolean):T[]{
    let res:T[] = [];
    list.reduce((prev, cur)=>{
        if(callback(cur)){
            res.push(cur);
        }
        return undefined;
    }, undefined);
    return res;
}
myList = [1,2,3,4,5];
let res1 = myFilter(myList, (item)=>item%2==1);
res1.forEach(item => {console.log(item)});
  
