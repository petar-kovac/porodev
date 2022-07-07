import { FC, useEffect, useState } from 'react';

import GroupCards from 'components/card/GroupCards';
import PFilter from 'components/filter/PFilter';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import { IFilesCard } from 'types/card-data';
import { usePageContext } from 'context/PageContext';

import RuntimeCards from 'components/card/RuntimeCards';
import {
  StyledPageWrapper,
  StyledContent,
  StyledFilterWrapper,
  StyledFilesWrapper,
  StyledStaticContent,
} from './runtime-styled';

const Runtime: FC = () => {
  const [isList, setIsList] = useState<boolean>(false);
  const [cardData, setCardData] = useState<IFilesCard | null>(null);
  const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

  const { setIsSiderVisible, setIsModalVisible } = usePageContext();

  return (
    <StyledPageWrapper>
      <StyledContent
        onClick={() => {
          setSelectedCardId(null);
          setCardData(null);
        }}
      >
        <StyledStaticContent>
          <StyledFilterWrapper>
            <PFilter
              isList={isList}
              setIsList={setIsList}
              activeFilters={{
                showSortByType: true,
                showSortByTime: true,
              }}
            />
          </StyledFilterWrapper>
          <StyledFilesWrapper>
            <RuntimeCards
              cardData={cardData}
              selectedCardId={selectedCardId}
              setCardData={setCardData}
              setSelectedCardId={setSelectedCardId}
              setIsModalVisible={setIsModalVisible}
              setIsSiderVisible={setIsSiderVisible}
            />
          </StyledFilesWrapper>
        </StyledStaticContent>

        <PFileSider cardData={cardData} type="runtime" />
      </StyledContent>
      <PModal cardData={cardData} setCardData={setCardData} />
    </StyledPageWrapper>
  );
};

export default Runtime;

// import axios from 'axios';
// import { FC, ReactNode, useEffect, useState } from 'react';

// import RuntimeCard from 'components/card/RuntimeCard';
// import PModal from 'components/modal/PModal';
// import { usePageContext } from 'context/PageContext';
// import PFileSider from 'layout/sider/PFileSider';
// import { startRuntimeService } from 'service/runtime/runtime';
// import { IFilesCard } from 'types/card-data';
// import { GetRuntimeModalData } from 'util/util-components/GetRuntimeModalData';
// import {
//   StyledContent,
//   StyledFilesWrapper,
//   StyledPageWrapper,
//   StyledStaticContent,
// } from './runtime-styled';

// const Runtime: FC = () => {
//   const [data, setData] = useState<IFilesCard[] | null>([]);
//   const [cardData, setCardData] = useState<IFilesCard | null>(null);

//   const {
//     setIsLoading,
//     setIsSiderVisible,
//     setIsModalVisible,
//     setNumberOfInputFields,
//     setInputParameters,
//     inputParameters,
//     setModalContent,
//     modalContent,
//   } = usePageContext();

//   const startRuntime = async () => {
//     setIsLoading(true);

//     try {
//       const res = await startRuntimeService({
//         projectId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
//         arguments: inputParameters,
//       });
//       const modalDataToRender: ReactNode = GetRuntimeModalData(res);
//       setModalContent(modalDataToRender);
//       setIsModalVisible(true);
//       setIsLoading(false);
//     } catch (err) {
//       setIsLoading(false);
//     }
//   };

//   useEffect(() => {
//     const fetchFiles = async () => {
//       try {
//         const res = await axios.get(`${process.env.REACT_APP_MOCK_URL}/files`);
//         setData(res.data);
//       } catch (err) {
//         console.log(err);
//       }
//     };
//     fetchFiles();
//   }, []);

//   return (
//     <StyledPageWrapper>
//       <StyledContent>
//         <StyledStaticContent
//           onClick={() => {
//             setCardData(null); // to trigger rerender, simulating onBlur effect
//             setInputParameters([]);
//             setNumberOfInputFields(1);
//           }}
//         >
//           <StyledFilesWrapper>
//             {data?.slice(0, 3).map((value: any, index) => (
//               <RuntimeCard
//                 key={value.id}
//                 title={value?.title}
//                 createdAt={value?.createdAt}
//                 selected={value?.id === cardData?.id}
//                 onClick={(e) => {
//                   setNumberOfInputFields(1);
//                   setInputParameters([]);
//                   setCardData(value);
//                   e.stopPropagation();
//                 }}
//                 onDoubleClick={(e) => {
//                   e.stopPropagation();
//                   setCardData(value);
//                   setIsModalVisible(true);
//                 }}
//               />
//             ))}
//           </StyledFilesWrapper>
//         </StyledStaticContent>

//         <PFileSider
//           type="runtime"
//           onButtonClick={startRuntime}
//           cardData={cardData}
//           setCardData={setCardData}
//           data={data}
//         />
//       </StyledContent>
//       <PModal
//         title="Result of you action: "
//         cardData={cardData}
//         setCardData={setCardData}
//         content={modalContent}
//       />
//     </StyledPageWrapper>
//   );
// };

// export default Runtime;
