import {
  createContext,
  Dispatch,
  FC,
  SetStateAction,
  useContext,
  useEffect,
  useMemo,
  useState,
} from 'react';

type PageContextProps = {
  isLoading: boolean;
  setIsLoading: Dispatch<SetStateAction<boolean>>;
  isModalVisible: boolean;
  setIsModalVisible: Dispatch<SetStateAction<boolean>>;
  isSiderVisible: boolean;
  setIsSiderVisible: Dispatch<SetStateAction<boolean>>;
  inputParameters: string[];
  setInputParameters: Dispatch<SetStateAction<string[]>>;
  numberOfInputFileds: number;
  setNumberOfInputFields: Dispatch<SetStateAction<number>>;
};

export const PageContext = createContext<PageContextProps>({
  isLoading: false,
  setIsLoading: () => undefined,
  isModalVisible: false,
  setIsModalVisible: () => undefined,
  isSiderVisible: false,
  setIsSiderVisible: () => undefined,
  inputParameters: [],
  setInputParameters: () => undefined,
  numberOfInputFileds: 1,
  setNumberOfInputFields: () => undefined,
});

export const PageConsumer = PageContext.Consumer;

const PageProvider: FC<any> = ({ children }) => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isSiderVisible, setIsSiderVisible] = useState<boolean>(false);

  const [inputParameters, setInputParameters] = useState<string[]>([]);
  const [numberOfInputFileds, setNumberOfInputFields] = useState<number>(1);

  const state: PageContextProps = useMemo(
    () => ({
      isLoading,
      setIsLoading,
      isModalVisible,
      setIsModalVisible,
      isSiderVisible,
      setIsSiderVisible,
      inputParameters,
      setInputParameters,
      numberOfInputFileds,
      setNumberOfInputFields,
    }),
    [
      isLoading,
      isModalVisible,
      isSiderVisible,
      inputParameters,
      numberOfInputFileds,
    ],
  );
  return <PageContext.Provider value={state}>{children}</PageContext.Provider>;
};

export const usePageContext: () => PageContextProps = () =>
  useContext(PageContext);

export default PageProvider;
