﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrlovMikhail.LJ.BookWriter
{
    public class ImagesExtraliner : ProcessorBase
    {
        protected internal override void ProcessInternal(List<PostPartBase> items)
        {
            for(int i = 0; i < items.Count; i++)
            {
                ImagePart ip = items[i] as ImagePart;
                if(ip == null)
                    continue;

                PostPartBase previous = (i > 0 ? items[i - 1] : null);
                PostPartBase next = (i < items.Count - 1 ? items[i + 1] : null);

                if(previous is LineBreakPart)
                    items[i - 1] = ParagraphStartPart.Instance;
                else if(previous != null && !(previous is ParagraphStartPart))
                {
                    items.Insert(i, ParagraphStartPart.Instance);
                    i++;
                }

                if(next is LineBreakPart)
                    items[i + 1] = ParagraphStartPart.Instance;
                else if(next != null && !(next is ParagraphStartPart))
                {
                    items.Insert(i + 1, ParagraphStartPart.Instance);

                    // Can skip, whatever.
                    i++;
                }
            }
        }
    }
}