import React, {useCallback} from 'react';
import logo from './logo.svg';
import './App.css';

interface BoxProps {
  children?: React.ReactNode;
}

const Heading = ({title}: {title: string})=>(
  <h2>{title}</h2>
)

//const Box = ({children}: {children: React.ReactNode}) => (
const Box:React.FC<BoxProps> = ({children}) => (
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

function App() {
  const onListClick = useCallback((item: string) => {
    alert(item);
  }, []);
  return (
    <div>
      <Heading title="Introduction" />
      <Box>
        Hello there
      </Box>
      <List items = {["one", "two", "three"]} onClick={onListClick}></List>
    </div>
  );
}

export default App;
