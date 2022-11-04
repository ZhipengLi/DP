import { useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'

function Counter(){

}
function App() {
  const [state, setState] = useState<{counter: number; theme: string}>({counter:4, theme:'blue'});
  function increaseCount(){
    setState((prevState)=>{
      return {...prevState, counter: prevState.counter+1};
    });
  }
  function decreaseCount(){
    setState((prevState)=>{
      return {...prevState, counter: prevState.counter-1};
    });
  }
  return (
    <>
      <button onClick={increaseCount}>+</button>
      {state.counter}
      <button onClick={decreaseCount}>-</button>
    </>
  )
}

export default App
