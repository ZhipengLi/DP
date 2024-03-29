// https://www.youtube.com/watch?v=XnyZXNnWAOA&list=PLNqp92_EXZBJYFrpEzdO2EapvU0GOJ09n

type Coordinate = {
  x: number,
  y: number
};
function parseCoordinate(str: string): Coordinate;
function parseCoordinate(obj: Coordinate): Coordinate;
function parseCoordinate(x: number, y: number): Coordinate;
function parseCoordinate(arg1: unknown, arg2?:unknown) : Coordinate {
  let coord: Coordinate = {
    x: 0,
    y: 0
  };
  if (typeof arg1 === 'object'){
    coord = {
      ...(arg1 as Coordinate)
    }
  }
  else if (typeof arg1==='string'){
    (arg1 as string).split(',').forEach(e=>{
      const [key, val] = e.split(':');
      coord[key as 'x' | 'y'] = parseInt(val);
    });
  }
  else {
    coord = {
      x: arg1 as number,
      y: arg2 as number
    }
  }
  return coord;
}

console.log(parseCoordinate(10, 20));
console.log(parseCoordinate({x: 52, y:35}));
console.log(parseCoordinate("x:12,y:20"));
