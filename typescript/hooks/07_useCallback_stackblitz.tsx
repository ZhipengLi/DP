// https://stackblitz.com/edit/react-ts-z8m7am?file=index.tsx

import { render } from 'react-dom';
import React, {Dispatch, useState, useReducer, useEffect, useCallback} from 'react';

function List({getItems}:{getItems:()=>number[]}){
  const [items, setItems] = useState([] as number[]);
  useEffect(()=>{
    setItems(getItems());
    console.log('Updating Items');
  }, [getItems]);
  return <div>{items.map(item =><div key={item}>{item}</div>)}</div>;
}

function App() {
  const [number, setNumber] = useState(1);
  const [dark, setDark] = useState(false);
  //const getItems = ()=>{
  //  return [number, number+1, number+2];
  //};
  const getItems = useCallback(()=>{
    return [number, number+1, number+2];
  }, [number]);
  const theme = {
    backgroundColor: dark? '#333': '#FFF',
    color: dark ? '#FFF' : '#333'
  };

  return (
    <div style={theme}>
      <input type="number" value={number} onChange={e => setNumber(parseInt(e.target.value))} />
      <button onClick={()=>setDark(prevDark => !prevDark)} >
        Toggle theme
      </button>
      <List getItems={getItems} />
    </div>
  );
}

render(<App />, document.getElementById('root'));
