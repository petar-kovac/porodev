import {
  createContext,
  Dispatch,
  FC,
  ReactNode,
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
  numberOfImages: number;
  setNumberOfImages: Dispatch<SetStateAction<number>>;
  modalContent: ReactNode;
  setModalContent: Dispatch<SetStateAction<ReactNode>>;
  parameters: IParameters;
  setParameters: Dispatch<SetStateAction<IParameters>>;
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

  numberOfImages: 0,
  setNumberOfImages: () => undefined,

  parameters: {
    numberOfImages: 0,
    imageList: [],
  },
  setParameters: () => undefined,
  modalContent: undefined,
  setModalContent: () => undefined,
});

export const PageConsumer = PageContext.Consumer;

interface IParameters {
  numberOfImages: number;
  imageList: string[];
}

const PageProvider: FC<any> = ({ children }) => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isSiderVisible, setIsSiderVisible] = useState<boolean>(true);

  const [inputParameters, setInputParameters] = useState<string[]>([]);
  const [numberOfInputFileds, setNumberOfInputFields] = useState<number>(1);

  const [parameters, setParameters] = useState<IParameters>({
    numberOfImages: 0,
    imageList: [],
  });
  const [numberOfImages, setNumberOfImages] = useState<number>(0);

  const [modalContent, setModalContent] = useState<ReactNode>(undefined);

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
      numberOfImages,
      setNumberOfImages,
      modalContent,
      setModalContent,
      parameters,
      setParameters,
    }),
    [
      isLoading,
      isModalVisible,
      isSiderVisible,
      inputParameters,
      numberOfInputFileds,
      numberOfImages,
      modalContent,
      parameters,
    ],
  );
  return <PageContext.Provider value={state}>{children}</PageContext.Provider>;
};

export const usePageContext: () => PageContextProps = () =>
  useContext(PageContext);

export default PageProvider;
