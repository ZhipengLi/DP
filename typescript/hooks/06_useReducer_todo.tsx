import React, { useReducer, useState, Dispatch } from "react";
import logo from "./logo.svg";
import "./App.css";
import { CombinedCodeActions, setTokenSourceMapRange } from "typescript";
import { time } from "console";
import { create } from "domain";

type TodoType = {
  id: number;
  name: string;
  completed: boolean;
};
enum ACTIONS {
  ADD_TODO,
  TOGGLE_TODO,
  REMOVE_TODO,
}
type ActionType =
  | { type: ACTIONS.ADD_TODO; payload: { name: string } }
  | { type: ACTIONS.TOGGLE_TODO; payload: { id: number } }
  | { type: ACTIONS.REMOVE_TODO; payload: { id: number } };
function Reducer(todos: TodoType[], action: ActionType): TodoType[] {
  switch (action.type) {
    case ACTIONS.ADD_TODO:
      const newTodo: TodoType = {
        id: Date.now(),
        name: action.payload.name,
        completed: false,
      };
      return [...todos, newTodo];
    case ACTIONS.TOGGLE_TODO:
      return todos.map((todo) => {
        if (todo.id === action.payload.id) {
          return { ...todo, completed: !todo.completed };
        }
        return todo;
      });
    case ACTIONS.REMOVE_TODO:
      return todos.filter((todo) => todo.id != action.payload.id);
    default:
      return todos;
  }
  return [];
}

function Todo({
  todo,
  dispatch,
}: {
  todo: TodoType;
  dispatch: Dispatch<ActionType>;
}) {
  return (
    <>
      <div style={{ color: todo.completed ? "#ccc" : "#000" }}>{todo.name}</div>
      <button
        onClick={() => {
          dispatch({ type: ACTIONS.TOGGLE_TODO, payload: { id: todo.id } });
        }}
      >
        Toggle
      </button>
      <button
        onClick={() => {
          dispatch({ type: ACTIONS.REMOVE_TODO, payload: { id: todo.id } });
        }}
      >
        X
      </button>
    </>
  );
}

function App() {
  const [todos, dispatch] = useReducer(Reducer, [] as TodoType[]);
  const [name, setName] = useState("");
  const onSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    dispatch({ type: ACTIONS.ADD_TODO, payload: { name: name } });
    setName("");
  };
  return (
    <>
      <form onSubmit={onSubmit}>
        <input
          type="text"
          value={name}
          onChange={(e) => {
            setName(e.target.value);
          }}
        />
      </form>
      {todos.map((todo) => {
        return <Todo key={todo.id} todo={todo} dispatch={dispatch}></Todo>;
      })}
    </>
  );
}

export default App;
