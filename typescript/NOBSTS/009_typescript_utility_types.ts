interface MyUser{
    name: string;
    id: string;
    email?: string;
    phone?: string;
}
type MyUserOptionals = Partial<MyUser>;
const merge = (user:MyUser, overrides: MyUserOptionals): MyUser =>{
    return {
        ...user,
        ...overrides,
    }
}
console.log(merge({
    name: "Jack",
    id: "foo",
    email: "dontemail@dontemail.com"
}, {
    email:"dontemailbaz@dontemail.com",
}));

type RequiredMyUser = Required<MyUser>;
type JustEmailAndName = Pick<MyUser, "email" | "name">;
type UserWithoutId = Omit<MyUser, "id">;
const mapById = (users:MyUser[]): Record<MyUser["id"], UserWithoutId>=>{
    return users.reduce((a,v)=>{
        const {id, ...other} = v;
        return {
            ...a,
            [id]:other,
        }
    }, {});
}

console.log(mapById([{
    id: "foo",
    name: "Mr.Foo"
},{
    id: "baz",
    name: "Mrs.Baz"
}]));
