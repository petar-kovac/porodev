import { message } from 'antd';
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
interface NewAction
  extends Action<
    | 'ADD_INPUT_FIELD'
    | 'CHANGE_INPUT_FIELD_VALUE'
    | 'DELETE_ALL_FIELDS'
    | 'FORMAT_FIELDS_FOR_SUBMITION'
    | 'DELETE_SINGLE_FIELD'
  > {
  payload?: {
    e?: ChangeEvent<HTMLInputElement>;
    index: number;
  };
}

type SiderContextProps = {
  inputParameters: string[];
  dispatchInput: Dispatch<NewAction>;
  imageParameters: string[];
  setImageParameters: Dispatch<SetStateAction<string[]>>;
};

export const GroupsContext = createContext<SiderContextProps>({
  inputParameters: [''],
  imageParameters: [],
  setImageParameters: () => undefined,
  dispatchInput: () => undefined,
});

const inputReducer = (state: string[], action: NewAction) => {
  switch (action.type) {
    case 'ADD_INPUT_FIELD': {
      if (state.length === 1 && state[0] === '') {
        message.error('Enter value in the field');
        return [...state];
      }
      return [...state, ''];
    }

    case 'CHANGE_INPUT_FIELD_VALUE': {
      if (action.payload) {
        state[action.payload.index] = action.payload.e?.target.value as string;
      }
      return [...state];
    }

    case 'DELETE_ALL_FIELDS':
      return [''];

    case 'DELETE_SINGLE_FIELD': {
      // cant delete input field, set its value to ''
      if (state.length === 1) {
        state[0] = '';
        return [...state];
      }
      if (action.payload) {
        state[action.payload.index] = '';
        return state.filter((value: string) => {
          return value !== '';
        });
      }
      return [...state];
    }

    case 'FORMAT_FIELDS_FOR_SUBMITION':
      return state.filter((value: string) => {
        return value !== '';
      });
    default:
      throw new Error('Bad reducer bro');
  }
};

const GroupsContextProvider: FC<{
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
    <GroupsContext.Provider value={state}>{children}</GroupsContext.Provider>
  );
};

export const useGroupsContext: () => SiderContextProps = () =>
  useContext(GroupsContext);

export default GroupsContextProvider;
