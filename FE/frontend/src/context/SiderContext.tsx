import {
  createContext,
  Dispatch,
  FC,
  ReactNode,
  SetStateAction,
  useContext,
  useMemo,
  useState,
} from 'react';

type SiderContextProps = {
  inputParameters: string[];
  setInputParameters: Dispatch<SetStateAction<string[]>>;
  imageParameters: string[];
  setImageParameters: Dispatch<SetStateAction<string[]>>;
};

export const SiderContext = createContext<SiderContextProps>({
  inputParameters: ['a'],
  setInputParameters: () => undefined,
  imageParameters: [],
  setImageParameters: () => undefined,
});

const SiderContextProvider: FC<{
  children: ReactNode;
}> = ({ children }) => {
  const [inputParameters, setInputParameters] = useState<string[]>(['']);
  const [imageParameters, setImageParameters] = useState<string[]>([]);

  const state: SiderContextProps = useMemo(
    () => ({
      inputParameters,
      setInputParameters,
      imageParameters,
      setImageParameters,
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
