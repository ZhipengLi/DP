let houses = [
  { "name": "Atreides", "planets": "Calladan" },
  { "name": "Corrino", "planets": ["Kaitan", "Salusa Secundus"] },
  { "name": "Harkonnen", "planets": ["Giedi Prime", "Arrakis"] }
];

interface House {
  name:string,
  planets: string | string[] 
}

interface HouseWithID extends House {
  id: string
}

function findHouses(houses: string): HouseWithID[];
function findHouses(
  houses: string,
  filter: (house: House) => boolean
): HouseWithID[];
function findHouses(houses: House[]): HouseWithID[];
function findHouses(
  houses: House[],
  filter: (house: House) => boolean
): HouseWithID[];
function findHouses(houses: unknown, filter?:(house:House)=>boolean): HouseWithID[]{
  let i=0;
  if (typeof houses === 'string'){
    houses = JSON.parse(houses);
  }
  let res: HouseWithID[] = (houses as Array<House>).map(h => ({...h, id: `${i++}`}));
  if (filter){
    res = res.filter(filter);
  }
  return res;
}

console.log(
  findHouses(JSON.stringify(houses), ({ name }) => name === "Atreides")
);

console.log(findHouses(houses, ({ name }) => name === "Harkonnen"));
