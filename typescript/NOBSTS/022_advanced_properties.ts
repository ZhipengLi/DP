import React, {useCallback, useEffect, useState, useReducer, useRef} from 'react';
import logo from './logo.svg';
import './App.css';

interface BoxProps {
  children?: React.ReactNode;
}

const Heading = ({title}: {title: string})=>(
  <h2>{title}</h2>
)

//const Box = ({children}: {children: React.ReactNode}) => (
const Box:React.FunctionComponent<BoxProps> = ({children}) => (
  <div style = {{
    padding: "1rem",
    fontWeight: "bold"
  }}>
    {children}
  </div>
)

const List:React.FC<{
  items: string[];
  onClick?: (item: string) => void
}> = ({items, onClick}) =>(
  <ul>
    {
      items.map((item, index) => (
      <li key={index} onClick={()=> onClick?.(item)}>{item}</li>
      ))
    }
  </ul>
);
interface Payload {
  text: string;
}

interface Todo {
  id: number;
  done: boolean;
  text: string;
}

type ActionType = 
  | {type: "ADD", text: string}
  | {type: "REMOVE", id: number};

const useNumber = (initialValue: number) => useState<number>(initialValue);
type UseNumberValue = ReturnType<typeof useNumber>[0];
type UseNumberSetValue = ReturnType<typeof useNumber>[1];

const Button: React.FunctionComponent<
  React.DetailedHTMLProps<
    React.ButtonHTMLAttributes<HTMLButtonElement>,
    HTMLButtonElement
> & {
  title? :string;
}
> = ({title, children, style, ...rest})=>(
  <button
    {...rest} style={{
      ...style, 
      backgroundColor:"red", 
      color: "white", 
      fontSize:"xx-large"
    }}
  >
    {title ?? children}
  </button>
);

const Incrementer: React.FunctionComponent<{
  value: UseNumberValue; //number;
  setValue: UseNumberSetValue;// React.Dispatch<React.SetStateAction<number>>
}> = ({value, setValue})=>(
  <Button onClick={()=> {setValue(value+1)}} title={`Add - ${value}`}>
    Add - {value}
  </Button>
);



function App() {
  const onListClick = useCallback((item: string) => {
    alert(item);
  }, []);

  const [payload, setPayload] = useState<Payload | null>(null);

  useEffect(()=>{
    console.log('state begin');
    fetch("/data.json")
    .then((resp) => resp.json())
    .then((data)=>{
      setPayload(data);
      console.log('state set');
    });
  }, []);

  const [todos, dispatch] = useReducer(
    (state: Todo[], action: ActionType) => {
      switch(action.type){
        case "ADD":
          return [
            ...state,
            {
              id: state.length,
              text: action.text,
              done: false,
            }
          ]
        case "REMOVE":
          return state.filter(({id})=>id !== action.id);
        default:
          throw new Error();
      }
  }, []);

  const newTodoRef = useRef<HTMLInputElement>(null);
  const onAddTodo = useCallback(()=>{
    if(newTodoRef.current){
      dispatch({
        type: "ADD",
        text: newTodoRef.current.value
      });
      newTodoRef.current.value = "";
    }

  }, []);

  const [value, setValue] = useNumber(0);// useState(0);

  return (
    <div>
      <Heading title="Introduction" />
      <Box>
        Hello there
      </Box>
      <List items = {["one", "two", "three"]} onClick={onListClick}></List>
      <Box>{JSON.stringify(payload)}</Box>
      <Incrementer value={value} setValue = {setValue} />
      <Heading title="Todos" />
      {todos.map((todo) => (
        <div key={todo.id}>
          {todo.text}
          <button onClick={()=>dispatch({
            type: "REMOVE",
            id: todo.id,
          })}>
          Remove
          </button>
        </div>
      ))}
      <div>
        <input type="text" ref={newTodoRef} />
        <Button onClick={onAddTodo}>Add Todo</Button>
      </div>
    </div>
  );
}

export default App;
