import { useState, useEffect, useRef } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'

function App() {
  const [name, setName] = useState('');
  const inputRef = useRef<HTMLInputElement>(null);
  const prevName = useRef<string>('');
  function focus(){
    if (inputRef.current){
      inputRef.current?.focus();
      inputRef.current.value = 'Some value';
    }
  }
  useEffect(()=>{
    prevName.current = name;
  }, [name]);
  return (<>
    <input ref={inputRef} value={name} onChange={e => {setName(e.target.value); }} />
    <div>My name is {name}</div>
    <div>My previous name is {prevName.current}</div>
    <button onClick={focus}>Focus</button>
  </>);
}

export default App
