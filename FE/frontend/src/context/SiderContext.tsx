import {
  ChangeEvent,
  createContext,
  Dispatch,
  FC,
  ReactNode,
  SetStateAction,
  useContext,
  useMemo,
  useReducer,
  useState,
} from 'react';

interface Action<T> {
  type: T;
}
interface NewAction extends Action<'ADD' | 'CHANGE' | 'DELETE' | 'SUBMIT'> {
  payload?: {
    e: ChangeEvent<HTMLInputElement>;
    index: number;
  };
}

type SiderContextProps = {
  inputParameters: string[];
  dispatchInput: Dispatch<NewAction>;
  imageParameters: string[];
  setImageParameters: Dispatch<SetStateAction<string[]>>;
};

export const SiderContext = createContext<SiderContextProps>({
  inputParameters: [''],
  imageParameters: [],
  setImageParameters: () => undefined,
  dispatchInput: () => undefined,
});

const inputReducer = (state: string[], action: NewAction) => {
  switch (action.type) {
    case 'ADD':
      return [...state, ''];
    case 'CHANGE': {
      const newItem = [...state];
      if (action.payload) {
        newItem[action.payload.index] = action.payload.e.target.value;
      }
      return [...newItem];
    }
    case 'DELETE':
      return [''];
    case 'SUBMIT':
      return state.filter((value: string) => {
        return value !== '';
      });
    default:
      throw new Error('Bad reducer bro');
  }
};

const SiderContextProvider: FC<{
  children: ReactNode;
}> = ({ children }) => {
  const [imageParameters, setImageParameters] = useState<string[]>([]);
  const [inputParameters, dispatchInput] = useReducer(inputReducer, ['']);

  const state: SiderContextProps = useMemo(
    () => ({
      inputParameters,
      imageParameters,
      setImageParameters,
      dispatchInput,
    }),
    [inputParameters, imageParameters],
  );
  return (
    <SiderContext.Provider value={state}>{children}</SiderContext.Provider>
  );
};

export const useSiderContext: () => SiderContextProps = () =>
  useContext(SiderContext);

export default SiderContextProvider;
