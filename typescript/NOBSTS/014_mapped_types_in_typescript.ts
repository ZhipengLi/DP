type MyFlexibleDogInfo = {
    name: string;
    [key: string]: string | number; // delcared for properties that are not defined ahead of time
}
const dog:MyFlexibleDogInfo = {
    name: "LG",
    breed: "Mutt",
    age: 22
};

interface DogInfo {
    name: string;
    age: number;
}

type OptionsFlags<Type> = {
    [Property in keyof Type]: null;
};

type DogInfoOptions = OptionsFlags<DogInfo>;

type Listerns<Type> = {
    [Property in keyof Type as `on${Capitalize<string & Property>}Change`]?: (
        newValue: Type[Property]
    )=>void;
} & {
    [Property in keyof Type as `on${Capitalize<string & Property>}Delete`]?: (
    )=>void;
};

function listenToObject<T>(obj: T, listeners: Listerns<T>): void {
    throw "Needs to be implemented";
}

const lg:DogInfo = {
    name: "LG",
    age: 13
}

type DogInfoListens = Listerns<DogInfo>;

listenToObject(lg, {
    onNameChange: (v: string) => {},
    onAgeChange: (v: number) => {},
    onAgeDelete: ()=>{}
});
