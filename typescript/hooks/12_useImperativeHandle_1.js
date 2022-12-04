import React, { useImperativeHandle } from 'react';

function CustomInput({ style, ...props }, ref) {
  useImperativeHandle(
    ref,
    () => {
      return { alertHi: () => alert('hi') };
    },
    []
  );
  return (
    <input
      ref={ref}
      {...props}
      style={{
        border: 'none',
        backgroundColor: 'lightpink',
        padding: '.25em',
        borderBottom: '.1em solid black',
        borderTopRightRadius: '.25em',
        borderTopLeftRadius: '.25em',
        ...style,
      }}
    />
  );
}

export default React.forwardRef(CustomInput);

/////////////////////////////////////////////////////////////////////////////////

import React, { useState, useRef, useImperativeHandle } from 'react';
import './style.css';
import CustomInput from './CustomInput';

export default function App() {
  const [value, setValue] = useState('red');
  const inputRef = useRef();

  return (
    <div>
      <CustomInput
        ref={inputRef}
        value={value}
        onChange={(e) => setValue(e.target.value)}
      />
      <br />
      <button onClick={() => inputRef.current.alertHi()}>Focus</button>
    </div>
  );
}
