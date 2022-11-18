// https://stackblitz.com/edit/react-ts-7918ul?file=index.tsx

import React, { Component, useState, useEffect,useRef, useLayoutEffect } from 'react';
import { render } from 'react-dom';

function App() {
  const [show, setShow] = useState(false);
  const popup = useRef<HTMLDivElement>();
  const button = useRef<HTMLButtonElement>();
  useLayoutEffect(()=>{
    if (popup.current == null || button.current == null)
      return;
    const {bottom} = button.current.getBoundingClientRect();
    popup.current.style.top = `${bottom+25}px`;
  }, [show]);

  return (
    <div>
      <button ref ={button} onClick={()=>setShow(prev=>!prev)}>
        Click Here
      </button>
      {show && (
        <div style={{position:"absolute"}} ref={popup}>
          This is a popup!
        </div>)
      }
    </div>
  );
}

render(<App />, document.getElementById('root'));
