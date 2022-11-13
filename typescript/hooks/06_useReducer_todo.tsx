import React, { useReducer, useState, Dispatch } from "react";
import logo from "./logo.svg";
import "./App.css";
import { CombinedCodeActions, setTokenSourceMapRange } from "typescript";

type TodoType = {
  id: number;
  name: string;
  completed: boolean;
};

type ActionType =
  | { type: ACTIONS.ADD_TODO; payload: { name: string } }
  | { type: ACTIONS.TOGGLE_TODO; payload: { id: number } }
  | { type: ACTIONS.DELETE_TODO; payload: { id: number } };

enum ACTIONS {
  ADD_TODO,
  TOGGLE_TODO,
  DELETE_TODO,
}

function reducer(todos: TodoType[], action: ActionType): TodoType[] {
  switch (action.type) {
    case ACTIONS.ADD_TODO:
      return [...todos, newTodo(action.payload.name)];
    case ACTIONS.TOGGLE_TODO:
      return todos.map((todo) => {
        if (todo.id === action.payload.id) {
          return { ...todo, completed: !todo.completed };
        }
        return todo;
      });
    case ACTIONS.DELETE_TODO:
      return todos.filter((todo) => todo.id != action.payload.id);
    default:
      return todos;
  }
}

function newTodo(name: string): TodoType {
  return { id: Date.now(), name: name, completed: false };
}

function Todo({
  todo,
  dispatch,
}: {
  todo: TodoType;
  dispatch: Dispatch<ActionType>;
}) {
  return (
    <div>
      <span style={{ color: todo.completed ? "#777" : "#000" }}>
        {todo.name}
      </span>
      <button
        onClick={() =>
          dispatch({ type: ACTIONS.TOGGLE_TODO, payload: { id: todo.id } })
        }
      >
        Toggle
      </button>
      <button
        onClick={() =>
          dispatch({ type: ACTIONS.DELETE_TODO, payload: { id: todo.id } })
        }
      >
        Delete
      </button>
    </div>
  );
}

function App() {
  const [todos, dispatch] = useReducer(reducer, [] as TodoType[]);
  const [name, setName] = useState("");

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    dispatch({ type: ACTIONS.ADD_TODO, payload: { name: name } });
    setName("");
  }

  console.log(todos);

  return (
    <>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
      </form>
      {todos.map((todo) => {
        return <Todo key={todo.id} todo={todo} dispatch={dispatch} />;
      })}
    </>
  );
}

export default App;
